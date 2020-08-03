using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Template.Extensions
{
    public static partial class Extensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection collection)
        {
            var serviceTypes = Assembly.GetExecutingAssembly().GetServiceTypes();

            foreach (var type in serviceTypes)
            {
                collection.AddSingleton(type);
            }

            return collection;
        }
    }
}
