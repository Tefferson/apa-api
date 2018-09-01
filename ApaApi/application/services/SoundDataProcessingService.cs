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

            var soundData = data
                .Take(result.Count)
                .Skip(reservedBytes)
                .ToArray();

            var matches = await _soundRecognitionService.RecognizeAsync(soundData);

            if (matches.Count() > 0)
            {
                var sensorId = SensorHelper.IdFromBytes(data);
                var sensorInfo = await _sensorInformationService.GetInformationAsync(sensorId);

                await _pushNotificationService.SendAsync(new PushNotificationModel
                {
                    Notification = new NotificationContentModel
                    {
                        Body = matches.First().Name,
                        Payload = sensorInfo.RoomTag,
                        Title = sensorInfo.LocationAlias
                    },
                    RegistrationIds = sensorInfo.ObservingDevices.Select(o => o.Token).ToArray()
                });
            }
        }
    }
}
