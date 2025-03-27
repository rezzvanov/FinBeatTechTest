using Microsoft.EntityFrameworkCore;

namespace SimpleDataApi.Storage
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        public DbSet<CodeValue> CodeValues { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CodeValue>(entity =>
            {
                entity.HasKey(p => p.Id)
                .IsClustered();

                entity.HasIndex(e => new { e.Code, e.Value })
                .IsClustered(false);
            });
        }
    }
}
