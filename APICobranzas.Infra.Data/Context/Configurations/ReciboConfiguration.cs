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
    public class ReciboConfiguration:IEntityTypeConfiguration<Recibo>
    {
        public void Configure(EntityTypeBuilder<Recibo> builder)
        {
            builder
                .Property(r => r.Fecha)
                .HasColumnType("smalldatetime");
            builder
                .Property(r => r.Total)
                .HasPrecision(10, 2)
                .IsRequired();
        }
    }
}
