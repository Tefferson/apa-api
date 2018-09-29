using application.interfaces.identity;
using application.interfaces.message;
using application.interfaces.sound_data_processing;
using application.interfaces.sound_recognition;
using application.services;
using application.settings;
using crosscutting.identity.intefaces;
using crosscutting.identity.models;
using crosscutting.identity.stores;
using domain.interfaces.repositories;
using infra.data.context;
using infra.data.repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace crosscutting.ioc.api
{
    public static class NativeInjectorBootstrapper
    {
        public static void RegisterServices(this IServiceCollection services, IConfigurationRoot configurationRoot)
        {
            // Identity
            services.AddSingleton<IUserStore<ApplicationUser>, UserStore>();
            services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromDays(1001);
            });

            //Infra data
            var connectionString = configurationRoot.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApaContext>(options =>
                options.UseSqlServer(connectionString), ServiceLifetime.Transient, ServiceLifetime.Transient);

            //Repositories
            services.RegisterRepositories();

            // Application services
            services.RegisterAppServices();

            // Settings
            services.ConfigureSettings(configurationRoot);

            // Auto mapper
            //services.AddAutoMapperSetup();
        }

        private static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddTransient<ISoundLabelRepository, SoundLabelRepository>();
            services.AddTransient<ISensorRepository, SensorRepository>();
            services.AddSingleton<IUserRepository, UserRespository>();
        }

        private static void RegisterAppServices(this IServiceCollection services)
        {
            services.AddTransient<IPushNotificationService, PushNotificationService>();
            services.AddTransient<ISoundDataProcessingService, SoundDataProcessingService>();
            services.AddTransient<ISoundRecognitionService, SoundRecognitionService>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddSingleton<INetworkProvider, NetworkProvider>();
        }

        private static void ConfigureSettings(this IServiceCollection services, IConfigurationRoot configurationRoot)
        {
            services.Configure<FireBaseSettings>(configurationRoot.GetSection("Firebase"));
        }
    }
}
