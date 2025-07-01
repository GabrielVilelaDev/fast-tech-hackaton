using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FastTech.Autenticacao.Domain.Entities;

namespace FastTech.Autenticacao.Infraestructure.Persistance.Mappings
{
    public class UsuarioConfiguracao : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Nome)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(u => u.Perfil)
                .IsRequired();

            builder.Property(u => u.Ativo)
                .IsRequired();

            builder.Property(u => u.CriadoEm)
                .IsRequired();

            builder.OwnsOne(u => u.Email, email =>
            {
                email.Property(e => e.Endereco)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnName("Email");
            });

            builder.OwnsOne(u => u.Senha, senha =>
            {
                senha.Property(s => s.Hash)
                    .IsRequired()
                    .HasMaxLength(512)
                    .HasColumnName("SenhaHash");
            });

            builder.OwnsOne(u => u.Cpf, cpf =>
            {
                cpf.Property(c => c.Numero)
                    .HasMaxLength(11)
                    .HasColumnName("Cpf")
                    .IsRequired(false);
            });

            builder.HasIndex("Email").IsUnique();
            builder.HasIndex("Cpf").IsUnique();
        }
    }
}
