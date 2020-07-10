using Microsoft.EntityFrameworkCore;

namespace Template.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) 
            : base(options)
        {
        }
    }
}
