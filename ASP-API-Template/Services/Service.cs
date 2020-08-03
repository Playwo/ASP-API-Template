using System.Threading.Tasks;

namespace Template.Services
{
    public abstract class Service
    {
        public virtual ValueTask InitializeAsync()
            => default;

        public virtual ValueTask RunAsync()
            => default;
    }
}
