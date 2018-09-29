using System.Threading.Tasks;
using ApaApi.configurations;
using ApaApi.controllers;
using ApaApi.helpers;
using ApaApi.models;
using application.dtos.identity;
using application.interfaces.identity;
using application.models.identity;
using crosscutting.identity.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApaApi.Controllers
{
    /// <summary>
    /// Define os métodos relacionados ao módulo de identidade da aplicação
    /// </summary>  
    [Route("identidade")]
    public class IdentityController : ApiController
    {
        private readonly IIdentityService _identityService;
        private readonly SigningConfiguration _signingConfiguration;
        private readonly TokenConfiguration _tokenConfiguration;

        /// <summary>
        /// Inicializa uma nova instância de IdentityController
        /// </summary>
        /// <param name="identityService">O serviço da identidade</param>
        /// <param name="signingConfiguration">As configurações de assinatura</param>
        /// <param name="tokenConfiguration">As configurações de token</param>
        public IdentityController(
            IIdentityService identityService,
            SigningConfiguration signingConfiguration,
            TokenConfiguration tokenConfiguration
        )
        {
            _identityService = identityService;
            _signingConfiguration = signingConfiguration;
            _tokenConfiguration = tokenConfiguration;
        }

        /// <summary>
        /// Criar um usuário.
        /// </summary>
        /// <param name="newUser">Os dados do novo usuário</param>
        [HttpPost]
        [AllowAnonymous]
        [Route("criar")]
        [ProducesResponseType(typeof(BaseViewModel), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Create([FromBody] CreateUserDTO newUser)
        {
            await _identityService.CreateAsync(newUser);

            return Response();
        }

        /// <summary>
        /// Autenticar um usuário para utilização dos recursos da API.
        /// </summary>
        /// <param name="credentials">As credenciais do usuário</param>
        [HttpPost]
        [AllowAnonymous]
        [Route("autenticar")]
        [ProducesResponseType(typeof(BaseViewModel<LoginModel>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Post([FromBody] CredentialsDTO credentials)
        {
            var user = await _identityService.Login(credentials);

            if (user == null)
            {
                return Response();
            }
            else
            {
                var responseData = BuildLoginResponse(user);
                return Response(responseData);
            }
        }

        private LoginModel BuildLoginResponse(ApplicationUser user)
        {
            var tokenData = JwtHelper.GenerateToken(user.Id, user.Email, _signingConfiguration, _tokenConfiguration);

            return new LoginModel
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                AccessToken = tokenData.AccessToken,
                CreationDate = tokenData.CreationDate,
                ExpirationDate = tokenData.ExpirationDate,
            };
        }
    }
}
