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

            builder.HasData(
                new
                {
                    Id = Guid.Parse("6ee54a46-007f-4e4c-9fe8-1a13eadf7fd1"),
                    Email = "cliente.padrao@padrao.com",
                    Cpf = Cliente.CPF_CLIENTE_PADRAO,
                    Nome = "Cliente Padrão"
                });
        }
    }
}
