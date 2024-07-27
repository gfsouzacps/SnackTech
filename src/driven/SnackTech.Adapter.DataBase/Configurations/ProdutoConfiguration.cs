using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SnackTech.Domain.Models;

namespace SnackTech.Adapter.DataBase.Configurations
{
    internal sealed class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable(nameof(Produto));

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .HasColumnType("varchar")
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.Descricao)
                .HasColumnType("varchar")
                .HasMaxLength(255);

            builder.Property(p => p.Valor)
                .IsRequired()
                .HasColumnType("smallmoney");
        }
    }
}
