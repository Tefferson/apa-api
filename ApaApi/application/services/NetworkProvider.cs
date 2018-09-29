using application.interfaces.sound_recognition;
using Encog.Neural.Networks;
using System.IO;

namespace application.services
{
    public class NetworkProvider : INetworkProvider
    {
        private readonly BasicNetwork _network;

        public NetworkProvider()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), @"nn-models\model.txt");
            using (var fs = new FileStream(path, FileMode.Open))
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
