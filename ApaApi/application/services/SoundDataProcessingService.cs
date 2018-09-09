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

namespace application.services
{
    public class SoundDataProcessingService : ISoundDataProcessingService
    {
        private readonly IPushNotificationService _pushNotificationService;
        private readonly ISoundRecognitionService _soundRecognitionService;
        private readonly ISensorInformationService _sensorInformationService;

        public SoundDataProcessingService(
            IPushNotificationService pushNotificationService,
            ISoundRecognitionService soundRecognitionService,
            ISensorInformationService sensorInformationService
            )
        {
            _pushNotificationService = pushNotificationService;
            _soundRecognitionService = soundRecognitionService;
            _sensorInformationService = sensorInformationService;
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
            var matches = _soundRecognitionService.RecognizeAsync(firstBlock).ToList();

            var secondBlock = normalizedSoundData.Skip(normalizedSoundData.Length / 2).ToArray();
            matches.AddRange(_soundRecognitionService.RecognizeAsync(secondBlock).ToList());

            var mostSimilar = matches.OrderByDescending(m => m.Match).First();
            if (mostSimilar.Match < 0.85) return;

            var sensorId = SensorHelper.IdFromBytes(data);
            var sensorInfo = await _sensorInformationService.GetInformationAsync(sensorId);

            await _pushNotificationService.SendAsync(new PushNotificationModel
            {
                Notification = new NotificationContentModel
                {
                    Body = mostSimilar.Name,
                    Payload = sensorInfo.RoomTag,
                    Title = sensorInfo.LocationAlias
                },
                RegistrationIds = sensorInfo.ObservingDevices.Select(o => o.Token).ToArray()
            });
        }
    }
}
