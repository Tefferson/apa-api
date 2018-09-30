using System.Collections.Generic;
using System.Threading.Tasks;
using ApaApi.controllers;
using ApaApi.models;
using application.interfaces.sound_log;
using domain.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApaApi.Controllers
{
    /// <summary>
    /// Define os métodos relacionados ao módulo de registro de som da aplicação
    /// </summary>  
    [Route("registro-de-som")]
    public class SoundLogController : ApiController
    {
        private readonly ISoundLogService _soundLogService;

        /// <summary>
        /// Inicializa uma nova instância de SoundLogController
        /// </summary>
        /// <param name="soundLogService">O serviço do registro de som</param>
        public SoundLogController(ISoundLogService soundLogService)
        {
            _soundLogService = soundLogService;
        }

        /// <summary>
        /// Listar os sons dos sensores do usuário.
        /// </summary>
        [HttpGet]
        [Authorize("Bearer")]
        [Route("listar")]
        [ProducesResponseType(typeof(BaseViewModel<IEnumerable<SoundLogModel>>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> List()
        {
            var events = await _soundLogService.ListByUserAsync(UserId);
            return Response(events);
        }
    }
}
