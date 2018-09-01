using application.models.sound_recognition;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace application.interfaces.sound_recognition
{
    public interface ISoundRecognitionService
    {
        Task<IEnumerable<RecognizedSoundModel>> RecognizeAsync(byte[] soundBytes);
    }
}
