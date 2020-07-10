using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace ASP_API_Template.Extensions
{
    public static partial class Extensions
    {
        public static void AddApplicationServices(this IServiceCollection collection)
        {
            var serviceTypes = Assembly.GetExecutingAssembly().GetServiceTypes();

            foreach (var type in serviceTypes)
            {
                collection.AddSingleton(type);
            }
        }
    }
}
