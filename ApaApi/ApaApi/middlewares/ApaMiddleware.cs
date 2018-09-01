using application.interfaces.sound_data_processing;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace ApaApi.middlewares
{
    public class ApaMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ISoundDataProcessingService _soundDataProcessingService;

        public ApaMiddleware(RequestDelegate next, ISoundDataProcessingService soundDataProcessingService)
        {
            _next = next;
            _soundDataProcessingService = soundDataProcessingService;
        }

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
            var buffer = new byte[4 * 1024];
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
