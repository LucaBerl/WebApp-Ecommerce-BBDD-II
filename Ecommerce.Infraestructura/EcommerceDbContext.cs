using Ecommerce.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infraestructura
{
    public class EcommerceDbContext : DbContext
    {
        public EcommerceDbContext(DbContextOptions<EcommerceDbContext> options) : base(options)
        {
        }

        // Propiedades para cada entidad:

        public DbSet<Producto> Productos { get; set; } = null!;
        public DbSet<Articulo> Articulos { get; set; } = null!;
        public DbSet<Categoria> Categorias { get; set; } = null!;
        public DbSet<Marca> Marcas { get; set; } = null!;
        public DbSet<ImagenArticulo> ImagenesArticulo { get; set; } = null!;
        public DbSet<Cliente> Clientes { get; set; } = null!;
        public DbSet<ClienteUsuario> ClienteUsuarios { get; set; } = null!;
        public DbSet<DomicilioCliente> DomiciliosCliente { get; set; } = null!;  
        public DbSet<Pedido> Pedidos { get; set; } = null!;
        public DbSet<DetallePedido> DetallesPedido { get; set; } = null!;
        public DbSet<Pago> Pagos { get; set; } = null!;
        public DbSet<Envio> Envios { get; set; } = null!;







        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // =============================================
            // TABLAS MAESTRAS
            // =============================================

            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.ToTable("CATEGORIA");
                entity.HasKey(c => c.Id);

                // Mapeo de la PK a la columna SQL (si no coinciden en mayúsculas)
                entity.Property(c => c.Id).HasColumnName("IDCategoria");

                entity.Property(c => c.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(c => c.Descripcion)
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Marca>(entity =>
            {
                entity.ToTable("MARCA");
                entity.HasKey(m => m.Id);
                entity.Property(m => m.Id).HasColumnName("IDMarca");

                entity.Property(m => m.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            // =============================================
            // PRODUCTO Y ARTICULO
            // =============================================

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.ToTable("PRODUCTO");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).HasColumnName("IDProducto");

                entity.Property(p => p.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(p => p.Descripcion)
                    .HasMaxLength(255);

                // Conversión del Enum 'GeneroProducto' a CHAR(1) en la BD
                entity.Property(p => p.Sexo)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasConversion<string>(); // Le dice a EF Core que guarde el string ("M", "F", "U") y no el int (0, 1, 2)

                // Replicamos el CHECK constraint
                entity.ToTable(tb => tb.HasCheckConstraint("CK_Producto_Sexo", "[Sexo] IN ('M','F','U')"));

                entity.Property(p => p.Estado)
                    .IsRequired()
                    .HasDefaultValue(true); // Replicamos el DEFAULT

                // Relaciones FK
                entity.HasOne(p => p.Categoria)
                    .WithMany(c => c.Productos)
                    .HasForeignKey(p => p.CategoriaId)
                    .HasConstraintName("FK_Producto_Categoria");

                entity.Property(p => p.CategoriaId).HasColumnName("IDCategoria");

                entity.HasOne(p => p.Marca)
                    .WithMany(m => m.Productos)
                    .HasForeignKey(p => p.MarcaId)
                    .HasConstraintName("FK_Producto_Marca");

                entity.Property(p => p.MarcaId).HasColumnName("IDMarca");
            });

            modelBuilder.Entity<Articulo>(entity =>
            {
                entity.ToTable("ARTICULO");
                entity.HasKey(a => a.Sku);
                entity.Property(a => a.Sku).HasColumnName("SKU");

                entity.Property(a => a.ProductoId).HasColumnName("IDProducto");

                entity.Property(a => a.Color)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(a => a.Talle)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(a => a.CantidadStock)
                    .IsRequired()
                    .HasDefaultValue(0);

                // Replicamos el CHECK
                entity.ToTable(tb => tb.HasCheckConstraint("CK_Articulo_Stock", "[CantidadStock] >= 0"));

                entity.Property(a => a.Precio)
                    .IsRequired()
                    .HasColumnType("decimal(10, 2)") // Tipo de dato exacto
                    .HasDefaultValue(0);

                // Replicamos el CHECK
                entity.ToTable(tb => tb.HasCheckConstraint("CK_Articulo_Precio", "[Precio] >= 0"));

                // Relación FK con ON DELETE CASCADE
                entity.HasOne(a => a.Producto)
                    .WithMany(p => p.Articulos)
                    .HasForeignKey(a => a.ProductoId)
                    .OnDelete(DeleteBehavior.Cascade) // Importante: ON DELETE CASCADE
                    .HasConstraintName("FK_Item_Producto");

                // Constraint UNIQUE compuesto
                entity.HasIndex(a => new { a.ProductoId, a.Color, a.Talle })
                    .IsUnique()
                    .HasDatabaseName("UQ_Item_Variante");
            });

            modelBuilder.Entity<ImagenArticulo>(entity =>
            {
                entity.ToTable("ImagenesArticulo");
                entity.HasKey(i => i.Id);
                entity.Property(i => i.Id).HasColumnName("IDImagen");

                entity.Property(i => i.Url)
                    .IsRequired()
                    .HasMaxLength(2083);

                entity.Property(i => i.ArticuloSku).HasColumnName("SKU");

                // Relación FK
                entity.HasOne(i => i.Articulo)
                    .WithMany(a => a.Imagenes)
                    .HasForeignKey(i => i.ArticuloSku)
                    .HasConstraintName("FK_Imagen_Sku");
            });

            // =============================================
            // CLIENTES Y USUARIOS
            // =============================================

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("CLIENTE");
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Id).HasColumnName("IDCliente");

                entity.Property(c => c.Nombre)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(c => c.Apellido)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(c => c.Telefono)
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<ClienteUsuario>(entity =>
            {
                entity.ToTable("ClienteUsuario");
                entity.HasKey(cu => cu.Id);
                entity.Property(cu => cu.Id).HasColumnName("IDUsuario");
                entity.Property(cu => cu.ClienteId).HasColumnName("IDCliente");

                entity.Property(cu => cu.Email)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(cu => cu.ContraseniaHash)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(cu => cu.FechaAlta)
                    .IsRequired()
                    .HasDefaultValueSql("GETDATE()"); // DEFAULT con función SQL

                entity.Property(cu => cu.Estado)
                    .IsRequired()
                    .HasDefaultValue(true);

                // Relación 1 a 1 con Cliente
                entity.HasOne(cu => cu.Cliente)
                    .WithOne(c => c.Usuario) // La prop. de navegación en Cliente
                    .HasForeignKey<ClienteUsuario>(cu => cu.ClienteId)
                    .HasConstraintName("FK_ClienteUsuario_Cliente");

                // Constraints UNIQUE
                entity.HasIndex(cu => cu.ClienteId).IsUnique(); // Por la relación 1 a 1
                entity.HasIndex(cu => cu.Email).IsUnique();
            });

            modelBuilder.Entity<DomicilioCliente>(entity =>
            {
                entity.ToTable("DomicilioCliente");
                entity.HasKey(d => d.Id);
                entity.Property(d => d.Id).HasColumnName("IDDomicilio");
                entity.Property(d => d.ClienteId).HasColumnName("IDCliente");

                entity.Property(d => d.Calle)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(d => d.Altura)
                    .HasMaxLength(20);

                entity.Property(d => d.Ciudad)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(d => d.Provincia)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(d => d.CodigoPostal)
                    .IsRequired()
                    .HasMaxLength(20);

                // Relación FK
                entity.HasOne(d => d.Cliente)
                    .WithMany(c => c.Domicilios)
                    .HasForeignKey(d => d.ClienteId)
                    .HasConstraintName("FK_Domicilio_Cliente");
            });

            // =============================================
            // PEDIDOS, DETALLES, PAGOS Y ENVÍOS
            // =============================================

            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.ToTable("PEDIDO");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).HasColumnName("IDPedido");
                entity.Property(p => p.ClienteId).HasColumnName("IDCliente");

                entity.Property(p => p.FechaCreacion)
                    .IsRequired()
                    .HasDefaultValueSql("GETDATE()");

                // Conversión del Enum 'EstadoPedido' a string
                entity.Property(p => p.Estado)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasConversion<string>()
                    .HasDefaultValue(Dominio.Enumeraciones.EstadoPedido.EnProceso); // Default con Enum

                // Replicamos el CHECK
                entity.ToTable(tb => tb.HasCheckConstraint("CK_Pedido_Estado", "[EstadoPedido] IN ('EnProceso', 'Finalizado')"));

                entity.Property(p => p.MontoTotal)
                    .HasColumnType("decimal(12, 2)");

                // Relación FK
                entity.HasOne(p => p.Cliente)
                    .WithMany(c => c.Pedidos)
                    .HasForeignKey(p => p.ClienteId)
                    .HasConstraintName("FK_Pedido_Cliente");
            });

            modelBuilder.Entity<DetallePedido>(entity =>
            {
                entity.ToTable("DetallePedido");
                entity.HasKey(d => d.Id);
                entity.Property(d => d.Id).HasColumnName("IDDetalle");
                entity.Property(d => d.PedidoId).HasColumnName("IDPedido");
                entity.Property(d => d.ArticuloSku).HasColumnName("SKU");

                entity.Property(d => d.Cantidad)
                    .IsRequired()
                    .HasColumnName("CantidadPedido"); // Mapeo de nombre de columna

                // Replicamos el CHECK
                entity.ToTable(tb => tb.HasCheckConstraint("CK_Detalle_Cantidad", "[CantidadPedido] > 0"));

                entity.Property(d => d.PrecioUnitario)
                    .IsRequired()
                    .HasColumnType("decimal(10, 2)");

                // Replicamos el CHECK
                entity.ToTable(tb => tb.HasCheckConstraint("CK_Detalle_Precio", "[PrecioUnitario] >= 0"));

                // Columna CALCULADA
                entity.Property(d => d.Subtotal)
                    .HasComputedColumnSql("[CantidadPedido] * [PrecioUnitario]", stored: true); // 'stored: true' es PERSISTED

                // Relaciones FK
                entity.HasOne(d => d.Pedido)
                    .WithMany(p => p.Detalles)
                    .HasForeignKey(d => d.PedidoId)
                    .HasConstraintName("FK_Detalle_Pedido");

                entity.HasOne(d => d.Articulo)
                    .WithMany(a => a.DetallesPedido)
                    .HasForeignKey(d => d.ArticuloSku)
                    .HasConstraintName("FK_Detalle_Item");
            });

            modelBuilder.Entity<Pago>(entity =>
            {
                entity.ToTable("PAGO");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).HasColumnName("IDPago");
                entity.Property(p => p.PedidoId).HasColumnName("IDPedido");

                entity.Property(p => p.MetodoPago)
                    .HasMaxLength(100);

                entity.Property(p => p.FechaPago)
                    .IsRequired()
                    .HasDefaultValueSql("GETDATE()");

                entity.Property(p => p.Monto)
                    .IsRequired()
                    .HasColumnType("decimal(12, 2)");

                // Replicamos el CHECK
                entity.ToTable(tb => tb.HasCheckConstraint("CK_Pago_Monto", "[Monto] >= 0"));

                // Relación 1 a 1 con Pedido
                entity.HasOne(p => p.Pedido)
                    .WithOne(pe => pe.Pago)
                    .HasForeignKey<Pago>(p => p.PedidoId)
                    .HasConstraintName("FK_Pago_Pedido");

                // Constraint UNIQUE
                entity.HasIndex(p => p.PedidoId).IsUnique();
            });

            modelBuilder.Entity<Envio>(entity =>
            {
                entity.ToTable("ENVIO");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("IDEnvio");
                entity.Property(e => e.PedidoId).HasColumnName("IDPedido");
                entity.Property(e => e.DomicilioId).HasColumnName("IDDomicilio");

                // Conversión del Enum 'EstadoEnvio' a string
                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasConversion<string>()
                    .HasDefaultValue(Dominio.Enumeraciones.EstadoEnvio.Pendiente);

                // Replicamos el CHECK
                entity.ToTable(tb => tb.HasCheckConstraint("CK_Envio_Estado", "[EstadoEnvio] IN ('Pendiente','EnTransito','Entregado','Cancelado')"));

                entity.Property(e => e.FechaUltMovimiento)
                    .IsRequired()
                    .HasDefaultValueSql("GETDATE()");

                entity.Property(e => e.Tracking)
                    .HasMaxLength(100);

                // Relaciones FK
                entity.HasOne(e => e.Pedido)
                    .WithMany(p => p.Envios)
                    .HasForeignKey(e => e.PedidoId)
                    .HasConstraintName("FK_Envio_Pedido");

                entity.HasOne(e => e.Domicilio)
                    .WithMany(d => d.Envios)
                    .HasForeignKey(e => e.DomicilioId)
                    .HasConstraintName("FK_Envio_Domicilio");
            });
        }
    }
}
