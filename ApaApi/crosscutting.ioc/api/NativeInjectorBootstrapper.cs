using application.interfaces.message;
using application.interfaces.sound_data_processing;
using application.interfaces.sound_recognition;
using application.services;
using application.settings;
using domain.interfaces.repositories;
using infra.data.context;
using infra.data.repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace crosscutting.ioc.api
{
    public static class NativeInjectorBootstrapper
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            // Configuration
            //var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var configurationRoot = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                //.AddJsonFile($"appsettings.{envName}.json", false, true)
                .Build();

            //Infra data
            var connectionString = configurationRoot.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApaContext>(options =>
                options.UseSqlServer(connectionString), ServiceLifetime.Transient, ServiceLifetime.Transient);
            //services.AddTransient(_ => new ApaContext(connectionString));

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
        }

        private static void RegisterAppServices(this IServiceCollection services)
        {
            services.AddTransient<IPushNotificationService, PushNotificationService>();
            services.AddTransient<ISoundDataProcessingService, SoundDataProcessingService>();
            services.AddTransient<ISoundRecognitionService, SoundRecognitionService>();
            services.AddSingleton<INetworkProvider, NetworkProvider>();
        }

        private static void ConfigureSettings(this IServiceCollection services, IConfigurationRoot configurationRoot)
        {
            services.Configure<FireBaseSettings>(configurationRoot.GetSection("Firebase"));
            services.Configure<MLSettings>(configurationRoot.GetSection("ML"));
        }
    }
}
