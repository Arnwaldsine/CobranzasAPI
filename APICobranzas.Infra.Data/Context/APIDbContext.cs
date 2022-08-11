using System.Collections.Generic;
using APICobranzas.Domain.Models;
using APICobranzas.Infra.Data.Extensions;
using Bogus;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace APICobranzas.Infra.Data.Context
{
    public class APIDbContext : DbContext
    {
        public APIDbContext(DbContextOptions options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<PuntoVenta>()
                .HasIndex(x => x.Numero)
                .IsUnique();
            builder.Entity<ObraSocial>()
                  .HasOne<TipoPrestador>(t => t.TipoPrestador)
                  .WithMany(o => o.ObrasSociales)
                  .HasForeignKey(t => t.TipoPrestadorId)
                  .OnDelete(DeleteBehavior.Cascade);
            ;
            builder.Entity<Usuario>()
                .HasOne<PuntoVenta>(p => p.PuntoVenta)
                .WithMany(u => u.Usuarios)
                .HasForeignKey(p => p.PuntoVentaId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.Entity<Gestion>()
                .Property(p => p.FechaContacto)
                .HasColumnType("smalldatetime");

            builder.Entity<Gestion>()
              .Property(p => p.FechaProxContacto)
              .HasColumnType("smalldatetime");

            builder.Entity<Gestion>()
                .HasOne<Contacto>(c => c.Contacto)
                .WithMany(g => g.Gestiones)
                .HasForeignKey(c => c.ContactoId);
            builder.Entity<Gestion>()
                .HasOne<ObraSocial>(c => c.ObraSocial)
                .WithMany(g => g.Gestiones)
                .HasForeignKey(c => c.ObraSocialId);
            builder.Entity<Gestion>()
                .HasOne<Respuesta>(r => r.Respuesta)
                .WithMany(g => g.Gestiones)
                .HasForeignKey(r => r.RespuestaId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
            builder.Entity<Gestion>()
                .HasOne<Usuario>(r => r.Usuario)
                .WithMany(g => g.Gestiones)
                .HasForeignKey(r => r.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
            builder.Entity<Factura>()
                .HasOne<PuntoVenta>(p => p.PuntoVenta)
                .WithMany(f => f.Facturas)
                .HasForeignKey(p => p.PuntoventaId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Factura>()
                .HasOne<Estado>(p => p.Estado)
                .WithMany(f => f.Facturas)
                .HasForeignKey(p => p.EstadoId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Factura>().Property(x => x.EstadoId).HasDefaultValue(1);
            builder.Entity<Factura>()
                .HasOne<ObraSocial>(p => p.ObraSocial)
                .WithMany(f => f.Facturas)
                .HasForeignKey(p => p.ObraSocialId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Factura>()
                .Property(f => f.ImporteCobrado)
                .HasPrecision(10, 2);
            builder.Entity<Factura>()
                .Property(f => f.ImporteFactura)
                .HasPrecision(10, 2);
            builder.Entity<Factura>()
                .Property(f => f.FechaAcuse)
                .HasColumnType("smalldatetime")
                .HasDefaultValueSql("GETDATE()");
            builder.Entity<Factura>()
              .Property(f => f.FechaEmision)
              .HasColumnType("smalldatetime")
              .HasDefaultValueSql("GETDATE()");
            builder.Entity<Factura>()
              .Property(f => f.FechaUltPago)
              .HasColumnType("smalldatetime");
            builder.Entity<Recibo>()
                .Property(r => r.Fecha)
                .HasColumnType("smalldatetime");
            builder.Entity<Recibo>()
                .Property(r => r.Total)
                .HasPrecision(10, 2)
                .IsRequired();
            builder.Entity<FacturaRecibo>()
                .HasOne<Banco>(f => f.Banco)
                .WithMany(g => g.FacturasRecibos)
                .HasForeignKey(f => f.BancoId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            builder.Entity<FacturaRecibo>()
                .HasOne<FormaPago>(f => f.FormaPago)
                .WithMany(a => a.FacturasRecibos)
                .HasForeignKey(f => f.FormaPagoId);
            builder.Entity<Factura>()
                .HasMany(x => x.NotasCredito)
                .WithMany(x => x.Facturas)
                .UsingEntity<FacturaNota>(
                   x=>x.HasOne(x=>x.NotaCredito)
                       .WithMany(x=>x.FacturasNotas)
                       .HasForeignKey(x=>x.NotaCreditoId),
                   x=>x.HasOne(x=>x.Factura).WithMany(x=>x.FacturasNotas)
                       .HasForeignKey(z=>z.FacturaId),
                   x=>x.HasKey(c=>new{c.FacturaId,c.NotaCreditoId})
                );
            builder.Entity<NotaCredito>()
                .Property(x => x.Total)
                .HasPrecision(10, 2);
                builder.Entity<Factura>()
                .HasMany(x => x.Recibos)
                .WithMany(x => x.Facturas)
                .UsingEntity<FacturaRecibo>(
                x => x.HasOne(x => x.Recibo)
                .WithMany(x=>x.FacturasRecibos).HasForeignKey(x => x.ReciboId).OnDelete(DeleteBehavior.Cascade),
                x => x.HasOne(x => x.Factura)
                .WithMany(z=>z.FacturasRecibos).HasForeignKey(x => x.FacturaId).OnDelete(DeleteBehavior.Cascade),
                x=>x.HasKey(c=>new { c.FacturaId, c.ReciboId})
                );
            builder.Entity<Factura>()
                .HasMany(x => x.NotasCredito)
                .WithMany(x => x.Facturas)
                .UsingEntity<FacturaNota>(
                x => x.HasOne(x => x.NotaCredito).WithMany(x => x.FacturasNotas).HasForeignKey(x => x.NotaCreditoId),
                x => x.HasOne(x => x.Factura).WithMany(x => x.FacturasNotas).HasForeignKey(x => x.FacturaId),
                x => x.HasKey(x => new { x.FacturaId, x.NotaCreditoId }
                ));
            builder.Entity<Factura>()
                .Property(p => p.ImporteDebe)
                .HasComputedColumnSql("[ImporteFactura] - [ImporteCobrado]", stored: true)
                .HasPrecision(10, 2);
            builder.Entity<NotaDebito>()
                .Property(z => z.Total)
                .HasPrecision(10, 2);
            builder.Entity<Factura>()
                .HasMany(x => x.Notasdebito)
                .WithMany(x => x.Facturas)
                .UsingEntity<FacturaDebito>(
                    z=>z.HasOne(x=>x.NotaDebito).WithMany(x=>x.FacturasDebitos).HasForeignKey(x=>x.NotaDebitoId),
                    z=>z.HasOne(x=>x.Factura).WithMany(x=>x.FacturasDebitos).HasForeignKey(z=>z.FacturaId),
                    x=>x.HasKey(x=> new{x.FacturaId,x.NotaDebitoId})
                );
                
            builder.Entity<FacturaRecibo>()
                .Property(z => z.SubTotal)
                .HasPrecision(10, 2);
            builder.Entity<FacturaNota>()
                .Property(x => x.Subtotal)
                .HasPrecision(10, 2);
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new RoleConfiguration());

            var idPV = 12;

            var puntosVenta = new Faker<PuntoVenta>()
                .RuleFor(z=>z.Id,z=>idPV++)
                .RuleFor(z => z.Numero, f => 12+idPV++)
                .RuleFor(z => z.Punto, f => f.Name.FullName());
            builder.Entity<PuntoVenta>()
                .HasData(puntosVenta.GenerateBetween(12, 12));



        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<NotaCredito> NotasCredito { get; set; }
        public DbSet<ObraSocial> ObrasSociales { get; set; }
        public DbSet<TipoPrestador> TiposPrestador { get; set; }
        public DbSet<Contacto> Contactos { get; set; }
        public DbSet<PuntoVenta> PuntosVenta { get; set; }
        public DbSet<Respuesta> Respuestas { get; set; }
        public DbSet<Gestion> Gestiones { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<FormaPago> FormasPago { get; set; }
        public DbSet<Banco> Bancos { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<Recibo> Recibos { get; set; }
      public DbSet<FacturaRecibo> FacturaRecibo { get; set; }
       public DbSet<FacturaNota> FacturaNota { get; set; }

    }
}
