using Microsoft.EntityFrameworkCore;
using WizCoPedidos.WebApi.Entidades;

namespace WizCoPedidos.WebApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Pedido> Pedidos => Set<Pedido>();
    public DbSet<ItemPedido> ItemsPedido => Set<ItemPedido>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.ToTable("Pedidos");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.ClienteNome)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(x => x.DataCriacao)
                .IsRequired();

            entity.Property(x => x.Status)
                .IsRequired();

            entity.Property(x => x.ValorTotal)
                .HasPrecision(18, 2);
        });

        modelBuilder.Entity<ItemPedido>(entity =>
        {
            entity.ToTable("ItensPedido");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.ProdutoNome)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(x => x.Quantidade)
                .IsRequired();

            entity.Property(x => x.PrecoUnitario)
                .HasPrecision(18, 2);

            entity.HasOne<Pedido>()
                .WithMany(p => p.ItemPedidos)
                .HasForeignKey(x => x.PedidoId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        base.OnModelCreating(modelBuilder);
    }
}
