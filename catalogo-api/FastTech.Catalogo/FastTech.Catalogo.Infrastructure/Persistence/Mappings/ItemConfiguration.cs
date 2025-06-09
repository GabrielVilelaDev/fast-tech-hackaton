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
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.ToTable("Item");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Nome)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(i => i.Descricao)
                .HasMaxLength(500);

            builder.OwnsOne(i => i.Preco, preco =>
            {
                preco.Property(p => p.Valor)
                     .HasColumnName("Preco")
                     .HasColumnType("decimal(10,2)")
                     .IsRequired();
            });

            builder.HasOne(i => i.TipoRefeicao)
                .WithMany()
                .HasForeignKey(i => i.TipoRefeicaoId);

            builder.Property(i => i.Ativo)
                .IsRequired();
        }
    }
}
