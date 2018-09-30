using System.Threading.Tasks;
using ApaApi.controllers;
using ApaApi.models;
using application.interfaces.sensor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApaApi.Controllers
{
    /// <summary>
    /// Define os métodos relacionados ao módulo de sensor da aplicação
    /// </summary>  
    [Route("sensor")]
    public class SensorController : ApiController
    {
        private readonly ISensorService _sensorService;

        /// <summary>
        /// Inicializa uma nova instância de SensorController
        /// </summary>
        /// <param name="sensorService">O serviço do sensor</param>
        public SensorController(ISensorService sensorService)
        {
            _sensorService = sensorService;
        }

        /// <summary>
        /// Subscrever o usuário para receber eventos do sensor.
        /// </summary>
        /// <param name="sensorId">O identificador do sensor</param>
        [HttpPost]
        [Authorize("Bearer")]
        [Route("subscrever")]
        [ProducesResponseType(typeof(BaseViewModel), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Subscribe([FromBody] string sensorId)
        {
            await _sensorService.Subscribe(sensorId, UserId);
            return Response();
        }

        /// <summary>
        /// Cancelar subscrição do usuário.
        /// </summary>
        /// <param name="sensorId">O identificador do sensor</param>
        [HttpPost]
        [Authorize("Bearer")]
        [Route("cancelar-subscricao")]
        [ProducesResponseType(typeof(BaseViewModel), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Unsubscribe([FromBody] string sensorId)
        {
            await _sensorService.Unsubscribe(sensorId, UserId);
            return Response();
        }
    }
}
