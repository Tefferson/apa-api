using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using application.interfaces.message;
using application.interfaces.sound_data_processing;
using application.models.message;

namespace application.services
{
    public class SoundDataProcessingService : ISoundDataProcessingService
    {
        private readonly IPushNotificationService _pushNotificationService;

        public SoundDataProcessingService(IPushNotificationService pushNotificationService)
        {
            _pushNotificationService = pushNotificationService;
        }

        public Task ProcessBytes(byte[] data, WebSocketReceiveResult result)
        {
            var reservedBytes = 12;

            var soundData = data
                .Take(result.Count)
                .Skip(reservedBytes)
                .ToArray();

            //TODO: teste
            var text = new string(soundData.Select(b => (char)b).ToArray());
            if (text == "pushandroid")
            {
                _pushNotificationService.Send(new PushNotificationModel
                {
                    Notification = new NotificationContentModel
                    {
                        Body = "Teste note",
                        Title = "titulo"
                    },
                    RegistrationIds = new[] { "d05T7WTY_mw:APA91bEvULvwYkb04yUEQHFjmsmum-smg5FaGvErbPYQTu6N2BzzV9zW9OqefTcNZ5bQsGf18qKZdCd5U0hJggoMU8c748wwbKZWmtkxTQNUyI82VRx9OZs_xvOs_jTNvsvqQRh5PDXu" }
                });
            }

            return Task.CompletedTask;
        }
    }
}
