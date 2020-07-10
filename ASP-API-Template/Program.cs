using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Template.Configuration;
using Template.Configuration.Options;
using Template.Extensions;
using YamlDotNet.Serialization;

namespace Template
{
    public class Program
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006")]
        public static async Task Main(string[] args)
        {
            var webHost = CreateHostBuilder(args).Build();

            await webHost.Services.InitializeServicesAsync();

            await webHost.StartAsync();
            await webHost.WaitForShutdownAsync();

            webHost.Dispose();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();

                    webBuilder.ConfigureKestrel((context, options) =>
                    {
                        var serverOptions = options.ApplicationServices.GetService<IOptionsMonitor<HttpOptions>>().CurrentValue;
                        options.Listen(serverOptions.GetAddress(), serverOptions.Port);
                    });
                })
                .ConfigureAppConfiguration((context, config) =>
                {
                    string path = $"{context.HostingEnvironment.EnvironmentName}.config.yaml";

                    TryCreateConfig(path);

                    config.Sources.Clear();
                    config.AddJsonFile("appsettings.json", false);
                    config.AddYamlFile(path, false);
                });

        public static void TryCreateConfig(string path)
        {
            if (!File.Exists(path))
            {
                string defaultConfig = new Serializer().Serialize(new
                {
                    Http = HttpOptions.Default,
                    Database = DatabaseOptions.Default,
                });

                File.WriteAllText(path, defaultConfig); ;
            }
        }
    }
}
