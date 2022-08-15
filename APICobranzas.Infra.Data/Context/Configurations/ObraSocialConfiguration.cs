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
    public class ObraSocialConfiguration : IEntityTypeConfiguration<ObraSocial>
    {
        public void Configure(EntityTypeBuilder<ObraSocial> builder)
        {
            builder
                .HasOne<TipoPrestador>(t => t.TipoPrestador)
                .WithMany(o => o.ObrasSociales)
                .HasForeignKey(t => t.TipoPrestadorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
