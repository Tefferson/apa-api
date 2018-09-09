using application.interfaces.sound_recognition;
using application.models.sound_recognition;
using Encog.ML.Data;
using Encog.ML.Data.Basic;
using Encog.Neural.Networks;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace application.services
{
    public class SoundRecognitionService : ISoundRecognitionService
    {
        private readonly BasicNetwork _network;

        public SoundRecognitionService(INetworkProvider networkProvider)
        {
            _network = networkProvider.GetNetwork();
        }

        public IEnumerable<RecognizedSoundModel> RecognizeAsync(double[] soundData)
        {
            var input = new BasicMLData(soundData, false);
            var output = _network.Compute(input);

            var result = new List<RecognizedSoundModel>();

            for (int i = 0; i < output.Count; i++)
            {
                result.Add(new RecognizedSoundModel
                {
                    Match = output[i],
                    Name = i.ToString()
                });
            }

            return result;
        }
    }
}
