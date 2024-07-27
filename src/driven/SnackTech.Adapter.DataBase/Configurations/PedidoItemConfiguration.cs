using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SnackTech.Domain.Models;

namespace SnackTech.Adapter.DataBase.Configurations
{
    internal sealed class PedidoItemConfiguration : IEntityTypeConfiguration<PedidoItem>
    {
        public void Configure(EntityTypeBuilder<PedidoItem> builder)
        {
            builder.ToTable(nameof(PedidoItem));

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Sequencial)
                .HasColumnType("int")
                .IsRequired();

            builder.Property(p => p.Quantidade)
                .HasColumnType("int")
                .IsRequired();

            builder.Property(p => p.Observacao)
                .HasColumnType("varchar")
                .HasMaxLength(500);

            builder.Property(p => p.Valor)
                .HasField("_valor")
                .UsePropertyAccessMode(PropertyAccessMode.PreferProperty)
                .IsRequired()
                .HasColumnType("smallmoney");

            builder.HasOne(item => item.Produto)
                .WithMany()
                .HasForeignKey(y => y.IdProduto);
        }
    }
}
