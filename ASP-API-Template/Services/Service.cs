using System.Threading.Tasks;

namespace ASP_API_Template.Services
{
    public abstract class Service
    {
        public virtual ValueTask InitializeAsync()
            => new ValueTask();
    }
}
