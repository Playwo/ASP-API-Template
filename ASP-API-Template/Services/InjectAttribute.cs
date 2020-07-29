using System;

namespace Template.Services
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class InjectAttribute : Attribute
    {
    }
}
