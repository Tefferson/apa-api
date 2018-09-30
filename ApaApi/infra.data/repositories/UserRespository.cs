using System.Threading;
using System.Threading.Tasks;
using crosscutting.identity.intefaces;
using crosscutting.identity.models;
using infra.data.context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace infra.data.repositories
{
    public class UserRespository : IUserRepository
    {
        private readonly ApaContext _context;

        public UserRespository(ApaContext context)
        {
            _context = context;
        }

        public async Task<IdentityResult> CreateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            try
            {
                await _context.ApplicationUser.AddAsync(user, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                _context.Entry(user).State = EntityState.Detached;
                return IdentityResult.Success;
            }
            catch
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Description = "Não foi possível inserir o usuário"
                });
            }
        }

        public Task<ApplicationUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
            => _context
                .ApplicationUser
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.NormalizedEmail == normalizedEmail, cancellationToken);

        public Task<ApplicationUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
            => _context
                .ApplicationUser
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == userId, cancellationToken);

        public Task<ApplicationUser> FindByUserNameAsync(string normalizedUserName, CancellationToken cancellationToken)
            => _context
                .ApplicationUser
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.NormalizedEmail == normalizedUserName, cancellationToken);
    }
}
