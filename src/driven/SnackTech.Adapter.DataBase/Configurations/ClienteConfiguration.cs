using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SnackTech.Domain.Models;

namespace SnackTech.Adapter.DataBase.Configurations
{
    internal sealed class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable(nameof(Cliente));

            builder.Property(c => c.Email)
                .HasColumnType("varchar")
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Cpf)
                .HasColumnType("varchar")
                .IsRequired()
                .HasMaxLength(15);

            builder.HasOne(c => c.Pessoa)
                .WithOne(p => p.Cliente);
        }
    }
}
