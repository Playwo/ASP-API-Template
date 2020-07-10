using Microsoft.EntityFrameworkCore;

namespace ASP_API_Template.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) 
            : base(options)
        {
        }
    }
}
