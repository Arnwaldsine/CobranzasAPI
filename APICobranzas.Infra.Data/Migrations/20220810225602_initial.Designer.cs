﻿// <auto-generated />
using System;
using APICobranzas.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace APICobranzas.Infra.Data.Migrations
{
    [DbContext(typeof(APIDbContext))]
    [Migration("20220810225602_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("APICobranzas.Domain.Models.Banco", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Bancos");
                });

            modelBuilder.Entity("APICobranzas.Domain.Models.Contacto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Tipo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Contactos");
                });

            modelBuilder.Entity("APICobranzas.Domain.Models.Estado", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Detalle")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Estados");
                });

            modelBuilder.Entity("APICobranzas.Domain.Models.Factura", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("EstadoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<DateTime?>("FechaAcuse")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smalldatetime")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<DateTime>("FechaEmision")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smalldatetime")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<DateTime?>("FechaUltPago")
                        .HasColumnType("smalldatetime");

                    b.Property<decimal>("ImporteCobrado")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.Property<decimal>("ImporteDebe")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)")
                        .HasComputedColumnSql("[ImporteFactura] - [ImporteCobrado]", true);

                    b.Property<decimal>("ImporteFactura")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.Property<int>("ObraSocialId")
                        .HasColumnType("int");

                    b.Property<string>("Observacion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PuntoventaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EstadoId");

                    b.HasIndex("ObraSocialId");

                    b.HasIndex("PuntoventaId");

                    b.ToTable("Facturas");
                });

            modelBuilder.Entity("APICobranzas.Domain.Models.FacturaDebito", b =>
                {
                    b.Property<int>("FacturaId")
                        .HasColumnType("int");

                    b.Property<int>("NotaDebitoId")
                        .HasColumnType("int");

                    b.Property<decimal>("Subtotal")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("FacturaId", "NotaDebitoId");

                    b.HasIndex("NotaDebitoId");

                    b.ToTable("FacturaDebito");
                });

            modelBuilder.Entity("APICobranzas.Domain.Models.FacturaNota", b =>
                {
                    b.Property<int>("FacturaId")
                        .HasColumnType("int");

                    b.Property<int>("NotaCreditoId")
                        .HasColumnType("int");

                    b.Property<int>("FormaPagoId")
                        .HasColumnType("int");

                    b.Property<decimal>("Subtotal")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("FacturaId", "NotaCreditoId");

                    b.HasIndex("FormaPagoId");

                    b.HasIndex("NotaCreditoId");

                    b.ToTable("FacturaNota");
                });

            modelBuilder.Entity("APICobranzas.Domain.Models.FacturaRecibo", b =>
                {
                    b.Property<int>("FacturaId")
                        .HasColumnType("int");

                    b.Property<int>("ReciboId")
                        .HasColumnType("int");

                    b.Property<int>("BancoId")
                        .HasColumnType("int");

                    b.Property<int>("FormaPagoId")
                        .HasColumnType("int");

                    b.Property<string>("NroChequeTransf")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NroReciboTes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("SubTotal")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("FacturaId", "ReciboId");

                    b.HasIndex("BancoId");

                    b.HasIndex("FormaPagoId");

                    b.HasIndex("ReciboId");

                    b.ToTable("FacturaRecibo");
                });

            modelBuilder.Entity("APICobranzas.Domain.Models.FormaPago", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Forma")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("FormasPago");
                });

            modelBuilder.Entity("APICobranzas.Domain.Models.Gestion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ContactoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaContacto")
                        .HasColumnType("smalldatetime");

                    b.Property<DateTime?>("FechaProxContacto")
                        .HasColumnType("smalldatetime");

                    b.Property<int>("ObraSocialId")
                        .HasColumnType("int");

                    b.Property<string>("Observacion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RespuestaId")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ContactoId");

                    b.HasIndex("ObraSocialId");

                    b.HasIndex("RespuestaId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Gestiones");
                });

            modelBuilder.Entity("APICobranzas.Domain.Models.NotaCredito", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Anulado")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<string>("Observaciones")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PuntoVentaId")
                        .HasColumnType("int");

                    b.Property<decimal>("Total")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("Id");

                    b.HasIndex("PuntoVentaId");

                    b.ToTable("NotasCredito");
                });

            modelBuilder.Entity("APICobranzas.Domain.Models.NotaDebito", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Anulado")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<string>("Observaciones")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Total")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("Id");

                    b.ToTable("NotaDebito");
                });

            modelBuilder.Entity("APICobranzas.Domain.Models.ObraSocial", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("CP")
                        .HasColumnType("bigint");

                    b.Property<string>("ContactoAdmin1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactoAdmin2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactoGeren1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactoGeren2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Cuit")
                        .HasColumnType("bigint");

                    b.Property<string>("Direccion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HorarioAdmin")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mailgeren")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Observaciones")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pagina")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Rnos")
                        .HasColumnType("bigint");

                    b.Property<string>("Tel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("TelAdmin")
                        .HasColumnType("bigint");

                    b.Property<long?>("TelGeren")
                        .HasColumnType("bigint");

                    b.Property<int>("TipoPrestadorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TipoPrestadorId");

                    b.ToTable("ObrasSociales");
                });

            modelBuilder.Entity("APICobranzas.Domain.Models.PuntoVenta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Numero")
                        .HasColumnType("int");

                    b.Property<string>("Punto")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Numero")
                        .IsUnique();

                    b.ToTable("PuntosVenta");

                    b.HasData(
                        new
                        {
                            Id = 12,
                            Numero = 25,
                            Punto = "Broderick Orn"
                        },
                        new
                        {
                            Id = 14,
                            Numero = 27,
                            Punto = "Marquise Blanda"
                        },
                        new
                        {
                            Id = 16,
                            Numero = 29,
                            Punto = "Harmon Dicki"
                        },
                        new
                        {
                            Id = 18,
                            Numero = 31,
                            Punto = "Edison Purdy"
                        },
                        new
                        {
                            Id = 20,
                            Numero = 33,
                            Punto = "Walker Dibbert"
                        },
                        new
                        {
                            Id = 22,
                            Numero = 35,
                            Punto = "Terrell Conroy"
                        },
                        new
                        {
                            Id = 24,
                            Numero = 37,
                            Punto = "Hadley Krajcik"
                        },
                        new
                        {
                            Id = 26,
                            Numero = 39,
                            Punto = "Thelma Daugherty"
                        },
                        new
                        {
                            Id = 28,
                            Numero = 41,
                            Punto = "Burdette Jones"
                        },
                        new
                        {
                            Id = 30,
                            Numero = 43,
                            Punto = "Lamont Batz"
                        },
                        new
                        {
                            Id = 32,
                            Numero = 45,
                            Punto = "Jason Sanford"
                        },
                        new
                        {
                            Id = 34,
                            Numero = 47,
                            Punto = "Nettie Ritchie"
                        });
                });

            modelBuilder.Entity("APICobranzas.Domain.Models.Recibo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Anulado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("smalldatetime");

                    b.Property<string>("Observaciones")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PuntoVentaId")
                        .HasColumnType("int");

                    b.Property<decimal>("Total")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("Id");

                    b.HasIndex("PuntoVentaId");

                    b.ToTable("Recibos");
                });

            modelBuilder.Entity("APICobranzas.Domain.Models.Respuesta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Detalle")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Respuestas");
                });

            modelBuilder.Entity("APICobranzas.Domain.Models.TipoPrestador", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TiposPrestador");
                });

            modelBuilder.Entity("APICobranzas.Domain.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("AceptaTerminos")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("Actualizado")
                        .HasColumnType("datetime2");

                    b.Property<string>("Apellido")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Creado")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("FechaExpiracionTokenReseteo")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaReseteoPassword")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaVerificion")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PuntoVentaId")
                        .HasColumnType("int");

                    b.Property<int>("Rol")
                        .HasColumnType("int");

                    b.Property<string>("TokenDeVerificacion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TokenReinicio")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PuntoVentaId");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("IdentityRole");

                    b.HasData(
                        new
                        {
                            Id = "dd610c55-6621-4695-82dd-6503433c3fc3",
                            ConcurrencyStamp = "31d03e4a-2265-485a-9431-4f4a0161446c",
                            Name = "Usuario",
                            NormalizedName = "USUARIO"
                        },
                        new
                        {
                            Id = "3c49a50c-fc49-40dc-8736-8e06f845d4f9",
                            ConcurrencyStamp = "a6c7fe90-d7ec-4760-9c02-1266f216c797",
                            Name = "Administrador",
                            NormalizedName = "ADMINISTRADOR"
                        });
                });

            modelBuilder.Entity("APICobranzas.Domain.Models.Factura", b =>
                {
                    b.HasOne("APICobranzas.Domain.Models.Estado", "Estado")
                        .WithMany("Facturas")
                        .HasForeignKey("EstadoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("APICobranzas.Domain.Models.ObraSocial", "ObraSocial")
                        .WithMany("Facturas")
                        .HasForeignKey("ObraSocialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("APICobranzas.Domain.Models.PuntoVenta", "PuntoVenta")
                        .WithMany("Facturas")
                        .HasForeignKey("PuntoventaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Estado");

                    b.Navigation("ObraSocial");

                    b.Navigation("PuntoVenta");
                });

            modelBuilder.Entity("APICobranzas.Domain.Models.FacturaDebito", b =>
                {
                    b.HasOne("APICobranzas.Domain.Models.Factura", "Factura")
                        .WithMany("FacturasDebitos")
                        .HasForeignKey("FacturaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("APICobranzas.Domain.Models.NotaDebito", "NotaDebito")
                        .WithMany("FacturasDebitos")
                        .HasForeignKey("NotaDebitoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Factura");

                    b.Navigation("NotaDebito");
                });

            modelBuilder.Entity("APICobranzas.Domain.Models.FacturaNota", b =>
                {
                    b.HasOne("APICobranzas.Domain.Models.Factura", "Factura")
                        .WithMany("FacturasNotas")
                        .HasForeignKey("FacturaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("APICobranzas.Domain.Models.FormaPago", "FormaPago")
                        .WithMany("FacturasNotas")
                        .HasForeignKey("FormaPagoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("APICobranzas.Domain.Models.NotaCredito", "NotaCredito")
                        .WithMany("FacturasNotas")
                        .HasForeignKey("NotaCreditoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Factura");

                    b.Navigation("FormaPago");

                    b.Navigation("NotaCredito");
                });

            modelBuilder.Entity("APICobranzas.Domain.Models.FacturaRecibo", b =>
                {
                    b.HasOne("APICobranzas.Domain.Models.Banco", "Banco")
                        .WithMany("FacturasRecibos")
                        .HasForeignKey("BancoId")
                        .IsRequired();

                    b.HasOne("APICobranzas.Domain.Models.Factura", "Factura")
                        .WithMany("FacturasRecibos")
                        .HasForeignKey("FacturaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("APICobranzas.Domain.Models.FormaPago", "FormaPago")
                        .WithMany("FacturasRecibos")
                        .HasForeignKey("FormaPagoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("APICobranzas.Domain.Models.Recibo", "Recibo")
                        .WithMany("FacturasRecibos")
                        .HasForeignKey("ReciboId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Banco");

                    b.Navigation("Factura");

                    b.Navigation("FormaPago");

                    b.Navigation("Recibo");
                });

            modelBuilder.Entity("APICobranzas.Domain.Models.Gestion", b =>
                {
                    b.HasOne("APICobranzas.Domain.Models.Contacto", "Contacto")
                        .WithMany("Gestiones")
                        .HasForeignKey("ContactoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("APICobranzas.Domain.Models.ObraSocial", "ObraSocial")
                        .WithMany("Gestiones")
                        .HasForeignKey("ObraSocialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("APICobranzas.Domain.Models.Respuesta", "Respuesta")
                        .WithMany("Gestiones")
                        .HasForeignKey("RespuestaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("APICobranzas.Domain.Models.Usuario", "Usuario")
                        .WithMany("Gestiones")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contacto");

                    b.Navigation("ObraSocial");

                    b.Navigation("Respuesta");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("APICobranzas.Domain.Models.NotaCredito", b =>
                {
                    b.HasOne("APICobranzas.Domain.Models.PuntoVenta", null)
                        .WithMany("NotasCredito")
                        .HasForeignKey("PuntoVentaId");
                });

            modelBuilder.Entity("APICobranzas.Domain.Models.ObraSocial", b =>
                {
                    b.HasOne("APICobranzas.Domain.Models.TipoPrestador", "TipoPrestador")
                        .WithMany("ObrasSociales")
                        .HasForeignKey("TipoPrestadorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TipoPrestador");
                });

            modelBuilder.Entity("APICobranzas.Domain.Models.Recibo", b =>
                {
                    b.HasOne("APICobranzas.Domain.Models.PuntoVenta", null)
                        .WithMany("Recibos")
                        .HasForeignKey("PuntoVentaId");
                });

            modelBuilder.Entity("APICobranzas.Domain.Models.Usuario", b =>
                {
                    b.HasOne("APICobranzas.Domain.Models.PuntoVenta", "PuntoVenta")
                        .WithMany("Usuarios")
                        .HasForeignKey("PuntoVentaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsMany("APICobranzas.Domain.Models.RefreshToken", "RefreshTokens", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<DateTime>("Created")
                                .HasColumnType("datetime2");

                            b1.Property<string>("CreatedByIp")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<DateTime>("Expires")
                                .HasColumnType("datetime2");

                            b1.Property<string>("ReplacedByToken")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<DateTime?>("Revoked")
                                .HasColumnType("datetime2");

                            b1.Property<string>("RevokedByIp")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Token")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<int>("UsuarioId")
                                .HasColumnType("int");

                            b1.HasKey("Id");

                            b1.HasIndex("UsuarioId");

                            b1.ToTable("RefreshToken");

                            b1.WithOwner("Usuario")
                                .HasForeignKey("UsuarioId");

                            b1.Navigation("Usuario");
                        });

                    b.Navigation("PuntoVenta");

                    b.Navigation("RefreshTokens");
                });

            modelBuilder.Entity("APICobranzas.Domain.Models.Banco", b =>
                {
                    b.Navigation("FacturasRecibos");
                });

            modelBuilder.Entity("APICobranzas.Domain.Models.Contacto", b =>
                {
                    b.Navigation("Gestiones");
                });

            modelBuilder.Entity("APICobranzas.Domain.Models.Estado", b =>
                {
                    b.Navigation("Facturas");
                });

            modelBuilder.Entity("APICobranzas.Domain.Models.Factura", b =>
                {
                    b.Navigation("FacturasDebitos");

                    b.Navigation("FacturasNotas");

                    b.Navigation("FacturasRecibos");
                });

            modelBuilder.Entity("APICobranzas.Domain.Models.FormaPago", b =>
                {
                    b.Navigation("FacturasNotas");

                    b.Navigation("FacturasRecibos");
                });

            modelBuilder.Entity("APICobranzas.Domain.Models.NotaCredito", b =>
                {
                    b.Navigation("FacturasNotas");
                });

            modelBuilder.Entity("APICobranzas.Domain.Models.NotaDebito", b =>
                {
                    b.Navigation("FacturasDebitos");
                });

            modelBuilder.Entity("APICobranzas.Domain.Models.ObraSocial", b =>
                {
                    b.Navigation("Facturas");

                    b.Navigation("Gestiones");
                });

            modelBuilder.Entity("APICobranzas.Domain.Models.PuntoVenta", b =>
                {
                    b.Navigation("Facturas");

                    b.Navigation("NotasCredito");

                    b.Navigation("Recibos");

                    b.Navigation("Usuarios");
                });

            modelBuilder.Entity("APICobranzas.Domain.Models.Recibo", b =>
                {
                    b.Navigation("FacturasRecibos");
                });

            modelBuilder.Entity("APICobranzas.Domain.Models.Respuesta", b =>
                {
                    b.Navigation("Gestiones");
                });

            modelBuilder.Entity("APICobranzas.Domain.Models.TipoPrestador", b =>
                {
                    b.Navigation("ObrasSociales");
                });

            modelBuilder.Entity("APICobranzas.Domain.Models.Usuario", b =>
                {
                    b.Navigation("Gestiones");
                });
#pragma warning restore 612, 618
        }
    }
}
