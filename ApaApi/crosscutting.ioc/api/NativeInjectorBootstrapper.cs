using application.interfaces.message;
using application.interfaces.sensor_information;
using application.interfaces.sound_data_processing;
using application.interfaces.sound_recognition;
using application.services;
using application.settings;
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

            // Application services
            services.RegisterAppServices();

            // Settings
            services.ConfigureSettings(configurationRoot);

            // Auto mapper
            //services.AddAutoMapperSetup();
        }

        private static void RegisterAppServices(this IServiceCollection services)
        {
            services.AddTransient<IPushNotificationService, PushNotificationService>();
            services.AddTransient<ISoundDataProcessingService, SoundDataProcessingService>();
            services.AddTransient<ISoundRecognitionService, SoundRecognitionService>();
            services.AddTransient<ISensorInformationService, SensorInformationService>();
        }

        private static void ConfigureSettings(this IServiceCollection services, IConfigurationRoot configurationRoot)
        {
            services.Configure<FireBaseSettings>(configurationRoot.GetSection("Firebase"));
        }
    }
}
