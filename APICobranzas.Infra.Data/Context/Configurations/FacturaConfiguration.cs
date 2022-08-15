using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APICobranzas.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APICobranzas.Infra.Data.Context.Configurations
{
    public class FacturaConfiguration:IEntityTypeConfiguration<Factura>
    {
        public void Configure(EntityTypeBuilder<Factura> builder)
        {
            builder
             .HasOne<PuntoVenta>(p => p.PuntoVenta)
             .WithMany(f => f.Facturas)
             .HasForeignKey(p => p.PuntoventaId)
             .OnDelete(DeleteBehavior.Cascade);
            builder
                .HasOne<Estado>(p => p.Estado)
                .WithMany(f => f.Facturas)
                .HasForeignKey(p => p.EstadoId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Property(x => x.EstadoId).HasDefaultValue(1);
            builder
                .HasOne<ObraSocial>(p => p.ObraSocial)
                .WithMany(f => f.Facturas)
                .HasForeignKey(p => p.ObraSocialId)
                .OnDelete(DeleteBehavior.Cascade);
            builder
                .Property(f => f.ImporteCobrado)
                .HasPrecision(10, 2);
            builder
                .Property(f => f.ImporteFactura)
                .HasPrecision(10, 2);
            builder
                .Property(f => f.FechaAcuse)
                .HasColumnType("smalldatetime")
                .HasDefaultValueSql("GETDATE()");
            builder
              .Property(f => f.FechaEmision)
              .HasColumnType("smalldatetime")
              .HasDefaultValueSql("GETDATE()");
            builder
              .Property(f => f.FechaUltPago)
              .HasColumnType("smalldatetime");
            builder
                            .HasMany(x => x.NotasCredito)
                            .WithMany(x => x.Facturas)
                            .UsingEntity<FacturaNota>(
                            x => x.HasOne(x => x.NotaCredito).WithMany(x => x.FacturasNotas).HasForeignKey(x => x.NotaCreditoId),
                            x => x.HasOne(x => x.Factura).WithMany(x => x.FacturasNotas).HasForeignKey(x => x.FacturaId),
                            x => x.HasKey(x => new { x.FacturaId, x.NotaCreditoId }
                            ));
            builder
                .Property(p => p.ImporteDebe)
                .HasComputedColumnSql("[ImporteFactura] - [ImporteCobrado]", stored: true)
                .HasPrecision(10, 2);
            builder
                .HasMany(x => x.Notasdebito)
                .WithMany(x => x.Facturas)
                .UsingEntity<FacturaDebito>(
                    z => z.HasOne(x => x.NotaDebito).WithMany(x => x.FacturasDebitos).HasForeignKey(x => x.NotaDebitoId),
                    z => z.HasOne(x => x.Factura).WithMany(x => x.FacturasDebitos).HasForeignKey(z => z.FacturaId),
                    x => x.HasKey(x => new { x.FacturaId, x.NotaDebitoId })
                );
            builder
                .HasMany(x => x.NotasCredito)
                .WithMany(x => x.Facturas)
                .UsingEntity<FacturaNota>(
                    x => x.HasOne(x => x.NotaCredito)
                        .WithMany(x => x.FacturasNotas)
                        .HasForeignKey(x => x.NotaCreditoId),
                    x => x.HasOne(x => x.Factura).WithMany(x => x.FacturasNotas)
                        .HasForeignKey(z => z.FacturaId),
                    x => x.HasKey(c => new { c.FacturaId, c.NotaCreditoId })
                );
            builder
                .HasMany(x => x.Recibos)
                .WithMany(x => x.Facturas)
                .UsingEntity<FacturaRecibo>(
                    x => x.HasOne(x => x.Recibo)
                        .WithMany(x => x.FacturasRecibos).HasForeignKey(x => x.ReciboId).OnDelete(DeleteBehavior.Cascade),
                    x => x.HasOne(x => x.Factura)
                        .WithMany(z => z.FacturasRecibos).HasForeignKey(x => x.FacturaId).OnDelete(DeleteBehavior.Cascade),
                    x => x.HasKey(c => new { c.FacturaId, c.ReciboId })
                );

        }
    }
}
