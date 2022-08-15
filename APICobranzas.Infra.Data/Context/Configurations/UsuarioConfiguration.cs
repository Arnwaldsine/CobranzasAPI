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
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {

            builder
                .HasOne<PuntoVenta>(p => p.PuntoVenta)
                .WithMany(u => u.Usuarios)
                .HasForeignKey(p => p.PuntoVentaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
