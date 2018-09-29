using System.Threading.Tasks;
using application.dtos.identity;
using application.interfaces.identity;
using crosscutting.identity.models;
using Microsoft.AspNetCore.Identity;

namespace application.services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IdentityService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> CreateAsync(CreateUserDTO newUser)
        {
            var identityResult = await _userManager.CreateAsync(new ApplicationUser
            {
                Email = newUser.Email,
                Name = newUser.Name,
                UserName = newUser.Email.Split('@')[0]
            }, newUser.Password);

            return identityResult.Succeeded == IdentityResult.Success.Succeeded;
        }

        public async Task<ApplicationUser> Login(CredentialsDTO credentials)
        {
            var user = await _userManager.FindByEmailAsync(credentials.Login);

            if (user == null) return null;

            var loginResult = await _signInManager.CheckPasswordSignInAsync(user, credentials.Password, false);

            return loginResult.Succeeded ? user : null;
        }
    }
}
