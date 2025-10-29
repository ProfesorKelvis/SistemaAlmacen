using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaAlmacen.Domain.Entities;


namespace SistemaAlmacen.Persistence.Settings
{
    public class AlmacenConfiguration : IEntityTypeConfiguration<Almacen>
    {
        public void Configure(EntityTypeBuilder<Almacen> builder)
        {
            // Tabla y clave
            builder.ToTable("Almacenes");
            builder.HasKey(a => a.Id);

            // Propiedades simples
            builder.Property(a => a.Nombre)
                    .HasColumnType("varchar(100)")
                    .HasMaxLength(100)
                    .IsRequired();

            builder.Property(a => a.Direccion)
                    .HasColumnType("varchar(300)")
                    .HasMaxLength(300)
                    .IsRequired();

            // Owned: Codigo
            builder.OwnsOne(a => a.Codigo, codigo =>
            {
                // Mapea la propiedad interna del VO
                codigo.Property(p => p.Valor)
                      .HasColumnName("Codigo")
                      .HasColumnType("varchar(5)")
                      .HasMaxLength(5)
                      .IsRequired();

                // Índice único sobre el valor del código
                codigo.HasIndex(p => p.Valor).IsUnique();
            });

            // Owned: Telefono
            builder.OwnsOne(a => a.Telefono, tel =>
            {
                tel.Property(p => p.Valor)
                   .HasColumnName("Telefono")
                   .HasColumnType("varchar(10)")
                   .HasMaxLength(10)
                   .IsRequired();
            });

            //// Relación con Localidad (muchos Almacenes -> una Localidad)
            //builder.HasOne(a => a.Localidad)
            //       .WithMany(l => l.Almacenes)              
            //       .HasForeignKey(a => a.LocalidadId)
            //       .OnDelete(DeleteBehavior.Restrict)
            //       .IsRequired();
        }
    }
}
