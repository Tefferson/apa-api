using System.Net.WebSockets;
using System.Threading.Tasks;

namespace application.interfaces.sound_data_processing
{
    public interface ISoundDataProcessingService
    {
        Task ProcessBytes(byte[] data, WebSocketReceiveResult result);
    }
}
