using application.interfaces.sound_data_processing;
using Microsoft.AspNetCore.Http;
using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace ApaApi.middlewares
{
    /// <summary>
    /// Define o middleware apa
    /// </summary>
    public class ApaMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ISoundDataProcessingService _soundDataProcessingService;

        /// <summary>
        /// Inicializa uma nova instância de ApMiddleware
        /// </summary>
        /// <param name="next"></param>
        /// <param name="soundDataProcessingService"></param>
        public ApaMiddleware(RequestDelegate next, ISoundDataProcessingService soundDataProcessingService)
        {
            _next = next;
            _soundDataProcessingService = soundDataProcessingService;
        }

        /// <summary>
        /// Direciona as requisições websocket
        /// </summary>
        /// <param name="context"></param>
        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path == "/" && context.WebSockets.IsWebSocketRequest)
            {
                await ListenAsync(context, await context.WebSockets.AcceptWebSocketAsync());
            }
            else { await _next(context); }
        }

        private async Task ListenAsync(HttpContext context, WebSocket webSocket)
        {
            var buffer = new byte[1024];
            var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            await _soundDataProcessingService.ProcessBytes(buffer, result);
            while (!result.CloseStatus.HasValue)
            {
                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                await _soundDataProcessingService.ProcessBytes(buffer, result);
            }
            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        }
    }
}
