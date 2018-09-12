using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ApaApi.configurations
{
    /// <summary>
    /// Provides the Web Api builder methods
    /// </summary>
    public static class WebApiServiceCollection
    {
        /// <summary>
        /// Adds a Web Api as a service
        /// </summary>
        /// <param name="services">The services</param>
        public static IMvcBuilder AddWebApi(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            var builder = services.AddMvcCore();
            builder.AddJsonFormatters();
            builder.AddApiExplorer();
            builder.AddCors();

            return new MvcBuilder(builder.Services, builder.PartManager);
        }

        /// <summary>
        /// Adds a Web Api as a service
        /// </summary>
        /// <param name="services">The services</param>
        /// <param name="setupAction">The action</param>
        public static IMvcBuilder AddWebApi(this IServiceCollection services, Action<MvcOptions> setupAction)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (setupAction == null) throw new ArgumentNullException(nameof(setupAction));

            var builder = services.AddWebApi();
            builder.Services.Configure(setupAction);

            return builder;
        }
    }
}
