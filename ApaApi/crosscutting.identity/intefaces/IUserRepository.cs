using System.Threading;
using System.Threading.Tasks;
using crosscutting.identity.models;
using Microsoft.AspNetCore.Identity;

namespace crosscutting.identity.intefaces
{
    public interface IUserRepository
    {
        Task<IdentityResult> CreateAsync(ApplicationUser user, CancellationToken cancellationToken);
        Task<ApplicationUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken);
        Task<ApplicationUser> FindByUserNameAsync(string normalizedUserName, CancellationToken cancellationToken);
        Task<ApplicationUser> FindByIdAsync(string userId, CancellationToken cancellationToken);
    }
}
