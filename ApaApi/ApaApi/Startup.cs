using System;
using ApaApi.middlewares;
using crosscutting.ioc.api;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ApaApi
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.RegisterServices();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(LogLevel.Debug);
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseWebSockets();

            app.UseWebSockets(new WebSocketOptions
            {
                KeepAliveInterval = TimeSpan.FromMinutes(2),
                ReceiveBufferSize = 4 * 1024
            });

            app.UseMiddleware<ApaMiddleware>();
        }
    }
}
