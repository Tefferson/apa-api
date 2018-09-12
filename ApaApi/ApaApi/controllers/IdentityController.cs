using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApaApi.Controllers
{
    /// <summary>
    /// Define os métodos relacionados ao módulo de identidade da aplicação
    /// </summary>  
    [Route("identidade")]
    public class IdentityController : ControllerBase
    {
        /// <summary>
        /// Autentica um usuário para utilização dos recursos da API.
        /// </summary>
        /// <param name="value">As credenciais do usuário</param>
        [HttpGet]
        [AllowAnonymous]
        [Route("autenticar")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Post([FromQuery] string value)
        {
            await Task.CompletedTask;
            return Ok(new { Message = "mensagem" });
        }
    }
}
