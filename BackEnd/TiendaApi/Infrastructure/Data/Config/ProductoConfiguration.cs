using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    internal class ProductoConfiguration :IEntityTypeConfiguration<Producto>
    {
        public void Configure(EntityTypeBuilder<Producto> builder)
        {
            builder.Property(p => p.Nombre).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Descripcion).IsRequired();
            builder.Property(p => p.Precio).HasColumnType("decimal(18,2)");
            builder.Property(p => p.FotoUrl).IsRequired();
        }
    }
}
