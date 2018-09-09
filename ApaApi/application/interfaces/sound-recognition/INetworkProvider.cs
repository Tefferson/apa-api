using Encog.Neural.Networks;

namespace application.interfaces.sound_recognition
{
    public interface INetworkProvider
    {
        BasicNetwork GetNetwork();
    }
}
