using Microsoft.EntityFrameworkCore;
using MinimalProductApi.Entities;

namespace MinimalProductApi.DbContexts
{
    public class ProductDbContext: DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> dbContextOptions)
            : base(dbContextOptions)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Products");
                entity.HasKey(p => p.Id).HasName("PK_Product");
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
