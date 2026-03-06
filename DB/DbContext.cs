using ApiPruebaVoultech.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiPruebaVoultech.Api.BD
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<OrdenCompra> OrdenCompras => Set<OrdenCompra>();
        public DbSet<Producto> Productos => Set<Producto>();
        public DbSet<OrdenProducto> OrdenProductos => Set<OrdenProducto>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrdenCompra>(e =>
            {
                e.ToTable("OrdenCompra");
                e.Property(x => x.Cliente)
                    .HasColumnName("Cliente")
                    .HasMaxLength(50)
                    .IsRequired();

                e.Property(x => x.FechaCreacion)
                    .HasColumnName("FechaCreacion")
                    .HasDefaultValueSql("GETDATE()");

                e.Property(x => x.Total)
                    .HasColumnName("Total")
                    .HasColumnType("decimal(19,2)");

                e.HasIndex(x => x.Cliente)
                    .HasDatabaseName("idx_cliente");
            });

            modelBuilder.Entity<Producto>(e =>
            {
                e.ToTable("Producto");

                e.Property(x => x.Nombre)
                    .HasColumnName("Nombre")
                    .HasMaxLength(50)
                    .IsRequired();

                e.Property(x => x.Precio)
                    .HasColumnName("Precio")
                    .HasColumnType("decimal(19,2)");

                e.HasIndex(x => x.Nombre)
                    .HasDatabaseName("idx_nombre");
            });

            modelBuilder.Entity<OrdenProducto>(e =>
            {
                e.ToTable("OrdenProducto");

                e.HasKey(x => x.Id);

                e.HasOne(x => x.OrdenCompra)
                    .WithMany(o => o.OrdenProductos)
                    .HasForeignKey(x => x.OrdenCompraId);

                e.HasOne(x => x.Producto)
                    .WithMany(p => p.OrdenProductos)
                    .HasForeignKey(x => x.ProductoId);
            });
        }
    }
}
