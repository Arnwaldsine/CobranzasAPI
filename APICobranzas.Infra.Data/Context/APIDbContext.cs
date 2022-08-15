using System.Collections.Generic;
using APICobranzas.Domain.Models;
using APICobranzas.Infra.Data.Context.Configurations;
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
            
            builder.ApplyConfiguration(new GestionConfiguration());
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new FacturaConfiguration());
            builder.ApplyConfiguration(new FacturaNotaConfiguration());
            builder.ApplyConfiguration(new FacturaReciboConfiguration());
            builder.ApplyConfiguration(new FacturaDebitoConfiguration());
            builder.ApplyConfiguration(new NotaCreditoConfiguration());
            builder.ApplyConfiguration(new NotaDebitoConfiguration());
            builder.ApplyConfiguration(new ObraSocialConfiguration());
            builder.ApplyConfiguration(new PuntoVentaConfiguration());
            builder.ApplyConfiguration(new ReciboConfiguration());
            builder.ApplyConfiguration(new UsuarioConfiguration());

            base.OnModelCreating(builder);

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
        public DbSet<FacturaDebito> FacturaDebito { get; set; }
    }
}
