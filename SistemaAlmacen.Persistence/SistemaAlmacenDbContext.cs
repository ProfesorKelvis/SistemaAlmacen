using Microsoft.EntityFrameworkCore;
using SistemaAlmacen.Domain.Entities;

namespace SistemaAlmacen.Persistence
{
    public class SistemaAlmacenDbContext : DbContext
    {
        public SistemaAlmacenDbContext(DbContextOptions<SistemaAlmacenDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SistemaAlmacenDbContext).Assembly);
        }

        public DbSet<Almacen> Almacenes { get; set; }

    }
}
