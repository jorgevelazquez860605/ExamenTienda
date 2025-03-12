using Microsoft.EntityFrameworkCore;
using Store.Data.Entities;


namespace Store.Data.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Tienda> Tiendas { get; set; }
    public DbSet<Articulo> Articulos { get; set; }
    public DbSet<ArticuloTienda> ArticuloTiendas { get; set; }
    public DbSet<ClienteArticulo> ClienteArticulos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {        
        modelBuilder.Entity<Tienda>().ToTable("Tiendas");
        modelBuilder.Entity<Articulo>().ToTable("Articulos");
        modelBuilder.Entity<ArticuloTienda>().ToTable("ArticuloTiendas");
        modelBuilder.Entity<ClienteArticulo>().ToTable("ClienteArticulos"); 
        modelBuilder.Entity<Cliente>().ToTable("Clientes");
        
        modelBuilder.Entity<ArticuloTienda>()
            .HasKey(at => new { at.ArticuloId, at.TiendaId });

        modelBuilder.Entity<ArticuloTienda>()
            .HasOne(at => at.Articulo)
            .WithMany(a => a.ArticuloTiendas)
            .HasForeignKey(at => at.ArticuloId);

        modelBuilder.Entity<ArticuloTienda>()
            .HasOne(at => at.Tienda)
            .WithMany(t => t.ArticuloTiendas)
            .HasForeignKey(at => at.TiendaId);

        
        modelBuilder.Entity<ClienteArticulo>()
            .HasKey(ca => ca.Id);

        modelBuilder.Entity<ClienteArticulo>()
            .HasOne(ca => ca.Cliente)
            .WithMany(c => c.ClienteArticulos)
            .HasForeignKey(ca => ca.ClienteId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ClienteArticulo>()
            .HasOne(ca => ca.Articulo)
            .WithMany(a => a.ClienteArticulos)
            .HasForeignKey(ca => ca.ArticuloId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ClienteArticulo>()
            .HasIndex(ca => ca.ClienteId);

        base.OnModelCreating(modelBuilder);

    }
}
