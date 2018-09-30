using System.Threading.Tasks;
using ApaApi.controllers;
using ApaApi.models;
using application.interfaces.device;
using application.interfaces.identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApaApi.Controllers
{
    /// <summary>
    /// Define os métodos relacionados ao módulo de dispositivo da aplicação
    /// </summary>  
    [Route("dispositivo")]
    public class DeviceController : ApiController
    {
        private readonly IDeviceService _deviceService;

        /// <summary>
        /// Inicializa uma nova instância de DeviceController
        /// </summary>
        /// <param name="deviceService">O serviço do dispositivo</param>
        public DeviceController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        /// <summary>
        /// Enviar o token do dispositivo para receber notificações.
        /// </summary>
        /// <param name="token">O token do dispositivo</param>
        [HttpPost]
        [Authorize("Bearer")]
        [Route("enviar-token")]
        [ProducesResponseType(typeof(BaseViewModel), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> SendToken([FromBody] string token)
        {
            await _deviceService.AddOrUpdateToken(token, UserId);
            return Response();
        }
    }
}
