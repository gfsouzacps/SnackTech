using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SnackTech.Domain.Models;

namespace SnackTech.Adapter.DataBase.Configurations
{
    internal sealed class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.ToTable(nameof(Pedido));

            builder.HasKey(p => p.Id);

            builder.Property(p => p.DataCriacao)
                .IsRequired()
                .HasColumnType("datetime");

            builder.Property(p => p.Status)
                .HasColumnType("int")
                .IsRequired();

            builder.Property(p => p.Valor)
                .IsRequired()
                .HasColumnType("smallmoney");

            builder.HasOne(p => p.Cliente)
                .WithMany(c => c.Pedidos);

            builder.HasMany(p => p.Itens)
                .WithOne(i => i.Pedido)
                .HasForeignKey(i => i.IdPedido);
        }
    }
}
