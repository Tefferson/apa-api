using application.interfaces.sound_recognition;
using application.models.sound_recognition;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace application.services
{
    public class SoundRecognitionService : ISoundRecognitionService
    {
        public async Task<IEnumerable<RecognizedSoundModel>> RecognizeAsync(byte[] soundBytes)
        {
            await Task.CompletedTask;
            return new List<RecognizedSoundModel> {
                new RecognizedSoundModel
                {
                    Match = 0.85,
                    Name = "Vidro quebrando"
                }
            };
        }
    }
}
