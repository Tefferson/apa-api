using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace ApaApi
{
    /// <summary>
    /// Define a classe Program
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Executa a aplicação
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args) => 
            CreateWebHostBuilder(args).Build().Run();

        /// <summary>
        /// Constrói o host
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>();
    }
}
