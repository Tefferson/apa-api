using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using application.interfaces.message;
using application.models.message;
using application.settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace application.services
{
    public class PushNotificationService : IPushNotificationService
    {
        private readonly FireBaseSettings _firebaseSettings;

        public PushNotificationService(IOptions<FireBaseSettings> firebaseOptions)
        {
            _firebaseSettings = firebaseOptions.Value;
        }

        public async Task SendAsync(PushNotificationModel content)
        {
            var httpWebRequest = WebRequest.Create(_firebaseSettings.Address);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Headers.Add("Authorization:key=" + _firebaseSettings.ServerKey);
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(await httpWebRequest.GetRequestStreamAsync()))
            {
                var settings = new JsonSerializerSettings
                {
                    ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new SnakeCaseNamingStrategy()
                    }
                };
                var json = JsonConvert.SerializeObject(content, settings);
                await streamWriter.WriteAsync(json);
                await streamWriter.FlushAsync();
            }

            var httpResponse = await httpWebRequest.GetResponseAsync();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = await streamReader.ReadToEndAsync();
            }
        }
    }
}
