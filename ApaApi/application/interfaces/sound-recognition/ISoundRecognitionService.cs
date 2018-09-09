using application.models.sound_recognition;
using System.Collections.Generic;

namespace application.interfaces.sound_recognition
{
    public interface ISoundRecognitionService
    {
        IEnumerable<RecognizedSoundModel> RecognizeAsync(double[] soundData);
    }
}
