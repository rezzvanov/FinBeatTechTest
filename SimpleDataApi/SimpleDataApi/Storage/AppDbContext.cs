using Microsoft.EntityFrameworkCore;

namespace SimpleDataApi.Storage
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            :base(options)
        {
            
        }

        public DbSet<CodeValue> CodeValues { get; set; } = null!;
    }
}
