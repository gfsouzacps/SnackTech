using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SnackTech.Domain.Models;

namespace SnackTech.Adapter.DataBase.Configurations
{
    [ExcludeFromCodeCoverage]
    internal sealed class PessoaConfiguration : IEntityTypeConfiguration<Pessoa>
    {
        public void Configure(EntityTypeBuilder<Pessoa> builder)
        {
            //Configura tabela por tipo de Pessoa
            builder.UseTptMappingStrategy();

            builder.ToTable(nameof(Pessoa));

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .HasColumnType("varchar")
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
