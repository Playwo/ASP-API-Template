using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Template.Configuration;
using YamlDotNet.Serialization;
using Common.Extensions;
using System.Reflection;
using System;
using Common.Configuration;
using Microsoft.Extensions.Logging;

namespace Template
{
    public class Program
    {
        public const string ConfigPath = "config.yaml";

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006")]
        public static async Task Main(string[] args)
        {
            bool generatedConfig = await TryCreateConfigAsync(ConfigPath);

            var webHost = CreateHostBuilder(args).Build();
            var logger = webHost.Services.GetRequiredService<ILogger<Startup>>();
            var assembly = Assembly.GetExecutingAssembly();

            if (generatedConfig)
            {
                logger.LogInformation($"Generated config file: {Path.Combine(Environment.CurrentDirectory, ConfigPath)}");
                // return;
            }

            var validationResult = await webHost.Services.ValidateOptionsAsync(assembly);

            if (!validationResult.IsSuccessful)
            {
                logger.LogError($"Config Validation failed: {validationResult.Reason}");
            }

            await webHost.Services.InitializeApplicationServicesAsync(assembly);
            webHost.Services.RunApplicationServices(assembly);        

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
                        var httpOptions = options.ApplicationServices.GetService<HttpOptions>();
                        options.Listen(httpOptions.GetAddress(), httpOptions.Port, listenOptions =>
                        {
                            if (httpOptions.Https)
                            {
                                listenOptions.UseHttps();
                            }
                        });

                    });
                })
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.Sources.Clear();
                    config.AddJsonFile("appsettings.json", false);
                });

        public static async Task<bool> TryCreateConfigAsync(string path)
        {
            if (File.Exists(path))
            {
                return false;
            }

            object config = Option.GenerateDefaultOptions();
            string defaultConfig = new Serializer().Serialize(config);
            await File.WriteAllTextAsync(path, defaultConfig);
            return true;
        }
    }
}
