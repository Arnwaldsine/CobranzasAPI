using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APICobranzas.Domain.Models;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APICobranzas.Infra.Data.Context.Configurations
{
   public class PuntoVentaConfiguration : IEntityTypeConfiguration<PuntoVenta>
    {
        public void Configure(EntityTypeBuilder<PuntoVenta> builder)
        {
            builder
                .HasIndex(x => x.Numero)
                .IsUnique();

            // SEEDING

            var idPV = 12;

            var puntosVenta = new Faker<PuntoVenta>()
                .RuleFor(z => z.Id, z => idPV++)
                .RuleFor(z => z.Numero, f => 12 + idPV++)
                .RuleFor(z => z.Punto, f => f.Name.FullName());
            builder
                .HasData(puntosVenta.GenerateBetween(12, 12));

        }
    }
}
