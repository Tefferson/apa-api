using application.interfaces.sound_recognition;
using application.settings;
using Encog.Neural.Networks;
using Microsoft.Extensions.Options;
using System.IO;

namespace application.services
{
    public class NetworkProvider : INetworkProvider
    {
        private readonly BasicNetwork _network;
        private readonly MLSettings _mlSettings;

        public NetworkProvider(IOptions<MLSettings> mlOptions)
        {
            _mlSettings = mlOptions.Value;
            using (var fs = new FileStream(_mlSettings.ModelPath, FileMode.Open))
            {
                _network = (BasicNetwork)new PersistBasicNetwork().Read(fs);
            }
        }

        public BasicNetwork GetNetwork()
        {
            return _network;
        }
    }
}
