using FastTech.Catalogo.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastTech.Catalogo.Infrastructure.Persistence.Mappings
{
    public class TipoRefeicaoConfiguration : IEntityTypeConfiguration<TipoRefeicao>
    {
        public void Configure(EntityTypeBuilder<TipoRefeicao> builder)
        {
            builder.ToTable("TipoRefeicao");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(250);
        }
    }
}
