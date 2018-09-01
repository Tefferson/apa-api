using System.Threading.Tasks;

namespace application.interfaces.message
{
    public interface IMessageService<T>
    {
        Task SendAsync(T content);
    }
}
