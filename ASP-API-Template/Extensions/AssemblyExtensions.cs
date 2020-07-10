using System;
using System.Linq;
using System.Reflection;
using Template.Services;

namespace Template.Extensions
{
    public static partial class Extensions
    {
        public static Type[] GetServiceTypes(this Assembly assembly)
            => assembly.GetTypes()
                .Where(x => x.BaseType == typeof(Service))
                .ToArray();
    }
}
