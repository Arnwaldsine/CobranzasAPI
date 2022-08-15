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
   public  class FacturaNotaConfiguration : IEntityTypeConfiguration<FacturaNota>
    {
        public void Configure(EntityTypeBuilder<FacturaNota> builder)
        {

            builder
                .Property(x => x.Subtotal)
                .HasPrecision(10, 2);
         
        }
    }
}
