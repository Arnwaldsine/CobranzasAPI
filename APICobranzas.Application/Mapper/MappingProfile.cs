using APICobranzas.Application.DTOs;
using APICobranzas.Domain.Models;

using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APICobranzas.Application.DTOs.Account;
using APICobranzas.Application.DTOs.Factura;
using APICobranzas.Application.DTOs.Gestion;
using APICobranzas.Application.DTOs.NotaCredito;
using APICobranzas.Application.DTOs.ObraSocial;
using APICobranzas.Application.DTOs.Recibo;
namespace APICobranzas.Application.Mapper
{
 

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ObraSocialRequestModel, ObraSocial>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Banco, BancoDTO>();
            CreateMap<FormaPago, FormaPagoDTO>();
            CreateMap<Recibo, ReciboDTO>();
            CreateMap<Recibo, ReciboDetalleDTO>();
            ;
            CreateMap<FacturaRecibo, DetalleRecibosFacturaDTO>();
            CreateMap<FacturaRecibo, DetalleFacturasReciboDTO>();
            CreateMap<Gestion, GestionDTO>()
                .ForMember(z => z.Contacto, o => o.MapFrom(x => x.Contacto.Tipo))
                .ForMember(z => z.Respuesta, o => o.MapFrom(x => x.Respuesta.Detalle));
            CreateMap<GestionRequest, Gestion>();
            CreateMap<Gestion, GestionPendienteDTO>()
                .ForMember(z => z.TotalDeuda, o => o.MapFrom(x => x.ObraSocial.Facturas.Sum(x => x.ImporteDebe)))
                .ForMember(x => x.ObraSocial, o => o.MapFrom(x => x.ObraSocial.Nombre))
                .ForMember(x => x.UltContacto, z => z.MapFrom(x => x.FechaContacto))
                .ForMember(x => x.FacturasSinCobrar, o => o.MapFrom(x => x.ObraSocial.Facturas.Count));
            CreateMap<TipoPrestador, TipoPrestadorDTO>();
            CreateMap<Factura, FacturaDTO>()
                .ForMember(x => x.Recibos, dest => dest.MapFrom(r => r.FacturasRecibos));
            CreateMap<Factura, FacturaItemDTO>()
                .ForMember(z => z.ObraSocial, opt => opt.MapFrom(x => x.ObraSocial.Nombre))
                .ForMember(x => x.PuntoVenta, opt => opt.MapFrom(x => x.PuntoVenta.Punto))
                .ForMember(z => z.Estado, opt => opt.MapFrom(z => z.Estado.Detalle));
            CreateMap<PuntoVenta, PuntoVentaDTO>();
            CreateMap<Estado, EstadoDTO>();
            CreateMap<ObraSocial, ObraSocialDTO>();
            CreateMap<Gestion, GestionDTO>()
                .ForMember(x => x.Usuario, o => o.MapFrom(z => z.Usuario.Nombre + " " + z.Usuario.Apellido))
                .ForMember(x => x.Contacto, a => a.MapFrom(x => x.Contacto.Tipo))
                .ForMember(x => x.Respuesta, o => o.MapFrom(x => x.Respuesta.Detalle));
            CreateMap<FacturaRequestModel, Factura>();
            CreateMap<ReciboRequestModel, Recibo>();
            CreateMap<FacturaReciboRequestModel, FacturaRecibo>();
            CreateMap<FacturaNota, DetalleFacturaNotaDTO>()
                .ForMember(x => x.FormaPago, opt => opt.MapFrom(x => x.FormaPago.Forma));
            CreateMap<Usuario, UsuarioResponse>();

            CreateMap<Usuario, LoginResponse>();
            CreateMap<Factura, DetalleFacturaGestionDTO>()
                .ForMember(x => x.PuntoVentaNro, o => o.MapFrom(x => x.PuntoVenta.Numero))
                .ForMember(x => x.PuntoVenta, o => o.MapFrom(z => z.PuntoVenta.Punto));

            CreateMap<RegistroRequest, Usuario>();
            CreateMap<NotaCredito, NotaCreditoDTO>()
                .ForMember(x => x.Detalles, opt => opt.MapFrom(z => z.FacturasNotas));
            CreateMap<CrearRequest, Usuario>();
            CreateMap<NotaCredito, NotaCreditoItemDTO>()
                .ForMember(z => z.ObraSocial,
                    opt => opt.MapFrom(x => x.FacturasNotas.First().Factura.ObraSocial.Nombre))
                .ForMember(x => x.PuntoVenta, opt => opt.MapFrom(Z => Z.Facturas.First().PuntoVenta.Punto));
            CreateMap<ModificarUsuarioRequest, Usuario>()
                .ForAllMembers(x => x.Condition(
                    (src, dest, prop) =>
                    {
                        // ignore null & empty string properties
                        if (prop == null) return false;
                        if (prop is string && string.IsNullOrEmpty((string) prop)) return false;

                        // ignore null role
                        if (x.DestinationMember.Name == "Rol" && src.Rol == null) return false;

                        return true;
                    }
                ));

            CreateMap<NotaCreditoRequest, NotaCredito>()
                .ForMember(z => z.FacturasNotas, opt => opt.MapFrom(x => x.Detalles));
            CreateMap<DetalleFacturaNotaRequest, FacturaNota>();

        }
    }
}
