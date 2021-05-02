using Microsoft.EntityFrameworkCore;
using PokemonShop.Data.Entities;

namespace PokemonShop.Data
{
    public sealed class PokemonShopDbContext : DbContext
    {
        public PokemonShopDbContext(DbContextOptions<PokemonShopDbContext> options) : base(options)
        {
            Database.Migrate();
        }
        
        public DbSet<Order> Orders => Set<Order>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Order>(x =>
            {
                x.ToTable("ORDERS");
                x.HasKey(p => p.Guid);
                x.Property(p => p.Guid).HasColumnName("GUID");
                x.Property(p => p.Name).HasColumnName("NAME");
                x.Property(p => p.Email).HasColumnName("EMAIL");
                x.Property(p => p.Telephone).HasColumnName("TELEPHONE");
                x.Property(p => p.StartDate).HasColumnName("START_DATE");
                
                // x.HasOne(k => k.Order).WithMany(k => k.Orders);
            });
        }
    }
}
