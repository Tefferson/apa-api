using System;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using application.Helpers;
using application.interfaces.message;
using application.interfaces.sensor_information;
using application.interfaces.sound_data_processing;
using application.interfaces.sound_recognition;
using application.models.message;
using domain.interfaces.repositories;

namespace application.services
{
    public class SoundDataProcessingService : ISoundDataProcessingService
    {
        private readonly IPushNotificationService _pushNotificationService;
        private readonly ISoundRecognitionService _soundRecognitionService;
        private readonly ISensorInformationService _sensorInformationService;
        private readonly ISoundLabelRepository _soundLabelRepository;

        public SoundDataProcessingService(
            IPushNotificationService pushNotificationService,
            ISoundRecognitionService soundRecognitionService,
            ISensorInformationService sensorInformationService,
            ISoundLabelRepository soundLabelRepository
            )
        {
            _pushNotificationService = pushNotificationService;
            _soundRecognitionService = soundRecognitionService;
            _sensorInformationService = sensorInformationService;
            _soundLabelRepository = soundLabelRepository;
        }

        public async Task ProcessBytes(byte[] data, WebSocketReceiveResult result)
        {
            var reservedBytes = 12;

            var soundChars = data
                .Take(result.Count)
                .Skip(reservedBytes)
                .Select(d => (char)d)
                .ToArray();

            var soundData = new string(soundChars)
                .Trim()
                .Split(' ')
                .Select(s => double.Parse(s));

            var normalizedSoundData = soundData
                .Select(d => d > 542 || d < 538 ? d : 0)
                .Select(d => d / 1024d)
                .ToArray();

            var firstBlock = normalizedSoundData.Take(normalizedSoundData.Length / 2).ToArray();
            var matches = _soundRecognitionService.Recognize(firstBlock).ToList();

            var secondBlock = normalizedSoundData.Skip(normalizedSoundData.Length / 2).ToArray();
            matches.AddRange(_soundRecognitionService.Recognize(secondBlock).ToList());

            var mostSimilar = matches.OrderByDescending(m => m.Match).First();
            if (mostSimilar.Match < 0.85) return;

            var sensorId = SensorHelper.IdFromBytes(data);
            var sensorInfo = await _sensorInformationService.GetInformationAsync(sensorId);

            var soundLabel = await _soundLabelRepository.GetByLabelNumberAsync(mostSimilar.LabelNumber);

            await _pushNotificationService.SendAsync(new PushNotificationModel
            {
                Notification = new NotificationContentModel
                {
                    Body = soundLabel?.LabelDescription,
                    Payload = sensorInfo.RoomTag,
                    Title = sensorInfo.LocationAlias
                },
                RegistrationIds = sensorInfo.ObservingDevices.Select(o => o.Token).ToArray()
            });
        }
    }
}
