using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Template.Services;

namespace Template.Extensions
{
    public static partial class Extensions
    {
        public static async Task InitializeServicesAsync(this IServiceProvider provider)
        {
            var serviceTypes = Assembly.GetEntryAssembly().GetServiceTypes();
            var loggerFactory = provider.GetRequiredService<ILoggerFactory>();

            foreach (var serviceType in serviceTypes)
            {
                var service = provider.GetRequiredService(serviceType) as Service;

                var serviceFields = serviceType.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic)
                    .Where(x => x.GetCustomAttribute(typeof(InjectAttribute)) != null);

                foreach (var field in serviceFields)
                {
                    var fieldType = field.FieldType;

                    object dependency = fieldType == typeof(ILogger)
                        ? loggerFactory.CreateLogger(serviceType)
                        : provider.GetRequiredService(fieldType);

                    field.SetValue(service, dependency);
                }

                await service.InitializeAsync();
            }
        }
    }
}
