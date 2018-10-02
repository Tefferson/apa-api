using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using application.Helpers;
using application.interfaces.message;
using application.interfaces.sound_data_processing;
using application.interfaces.sound_recognition;
using application.models.message;
using application.models.sound_recognition;
using domain.interfaces.repositories;

namespace application.services
{
    public class SoundDataProcessingService : ISoundDataProcessingService
    {
        private readonly IRecognizedSoundLogRepository _soundLogRepository;
        private readonly IPushNotificationService _pushNotificationService;
        private readonly ISoundRecognitionService _soundRecognitionService;
        private readonly ISoundLabelRepository _soundLabelRepository;
        private readonly ISensorRepository _sensorRepository;

        public SoundDataProcessingService(
            IRecognizedSoundLogRepository soundLogRepository,
            IPushNotificationService pushNotificationService,
            ISoundRecognitionService soundRecognitionService,
            ISoundLabelRepository soundLabelRepository,
            ISensorRepository sensorRepository
        )
        {
            _pushNotificationService = pushNotificationService;
            _soundRecognitionService = soundRecognitionService;
            _soundLabelRepository = soundLabelRepository;
            _soundLogRepository = soundLogRepository;
            _sensorRepository = sensorRepository;
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
            if (mostSimilar.Match >= 0.6 && mostSimilar.Match <= 0.85) return;

            var sensorId = SensorHelper.IdFromBytes(data);
            var sensorInfo = await _sensorRepository.FindAsync(sensorId);

            var soundLabel = await _soundLabelRepository.GetByLabelNumberAsync(mostSimilar.LabelNumber);

            await _pushNotificationService.SendAsync(new PushNotificationModel
            {
                Notification = new NotificationContentModel
                {
                    Body = soundLabel?.LabelDescription,
                    Payload = sensorInfo.RoomTag,
                    Title = sensorInfo.PlaceAlias
                },
                RegistrationIds = sensorInfo.ObservingDevices?.Select(o => o.Device.Token).ToArray()
            });

            await _soundLogRepository.CreateAsync(sensorId, soundLabel.LabelNumber, mostSimilar.Match);
        }
    }
}
