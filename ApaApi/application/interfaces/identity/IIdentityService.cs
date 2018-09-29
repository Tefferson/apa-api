using application.dtos.identity;
using crosscutting.identity.models;
using System.Threading.Tasks;

namespace application.interfaces.identity
{
    public interface IIdentityService
    {
        Task<ApplicationUser> Login(CredentialsDTO credentials);
        Task<bool> CreateAsync(CreateUserDTO newUser);
    }
}
