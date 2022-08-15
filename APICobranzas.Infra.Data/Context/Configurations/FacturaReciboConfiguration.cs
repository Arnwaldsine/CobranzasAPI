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
    public class FacturaReciboConfiguration: IEntityTypeConfiguration<FacturaRecibo>
    {
        public void Configure(EntityTypeBuilder<FacturaRecibo> builder)
        {
            builder
                .HasOne<Banco>(f => f.Banco)
                .WithMany(g => g.FacturasRecibos)
                .HasForeignKey(f => f.BancoId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            builder
                .HasOne<FormaPago>(f => f.FormaPago)
                .WithMany(a => a.FacturasRecibos)
                .HasForeignKey(f => f.FormaPagoId);


            builder
                .Property(z => z.SubTotal)
                .HasPrecision(10, 2);

        }
    }
}
