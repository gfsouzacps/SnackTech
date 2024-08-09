using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SnackTech.Adapter.DataBase.Entities;

namespace SnackTech.Adapter.DataBase.Configurations
{
    [ExcludeFromCodeCoverage]
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

            builder.Navigation(nameof(Pedido.Itens));
        }
    }
}
