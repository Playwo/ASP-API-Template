using System;
using System.Linq;
using System.Reflection;
using ASP_API_Template.Services;

namespace ASP_API_Template.Extensions
{
    public static partial class Extensions
    {
        public static Type[] GetServiceTypes(this Assembly assembly)
            => assembly.GetTypes()
                .Where(x => x.BaseType == typeof(Service))
                .ToArray();
    }
}
