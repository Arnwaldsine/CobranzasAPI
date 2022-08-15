using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APICobranzas.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APICobranzas.Infra.Data.Context.Configurations
{
    public class GestionConfiguration: IEntityTypeConfiguration<Gestion>
    {
        public void Configure(EntityTypeBuilder<Gestion> builder)
        {

            builder
                .Property(p => p.FechaContacto)
                .HasColumnType("smalldatetime");

            builder
                .Property(p => p.FechaProxContacto)
                .HasColumnType("smalldatetime");

            builder
                .HasOne<Contacto>(c => c.Contacto)
                .WithMany(g => g.Gestiones)
                .HasForeignKey(c => c.ContactoId);
            builder
                .HasOne<ObraSocial>(c => c.ObraSocial)
                .WithMany(g => g.Gestiones)
                .HasForeignKey(c => c.ObraSocialId);
            builder
                .HasOne<Respuesta>(r => r.Respuesta)
                .WithMany(g => g.Gestiones)
                .HasForeignKey(r => r.RespuestaId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
            builder
                .HasOne<Usuario>(r => r.Usuario)
                .WithMany(g => g.Gestiones)
                .HasForeignKey(r => r.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
