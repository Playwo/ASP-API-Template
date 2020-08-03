using System.Threading.Tasks;

namespace Template.Services
{
    public abstract class Service
    {
        public virtual ValueTask InitializeAsync()
            => new ValueTask();

        public virtual ValueTask RunAsync()
            => new ValueTask();
    }
}
