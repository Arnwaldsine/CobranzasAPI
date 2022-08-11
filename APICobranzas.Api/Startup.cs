using APICobranzas.Application.Interfaces;
using APICobranzas.Application.Mapper;
using APICobranzas.Application.Services;
using APICobranzas.Infra.Data.Context;
using APICobranzas.Infra.Data.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using APICobranzas.Application.Helpers;
using APICobranzas.Application.Middleware;
using APICobranzas.Domain.Interfaces;
using APICobranzas.Infra.Data.Middleware;

namespace APICobranzas.Api 
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(
                mc =>
                {
                    mc.AddProfile(new MappingProfile());
                });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddDbContext<APIDbContext>(
                options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                    options.EnableSensitiveDataLogging(true);
                },ServiceLifetime.Transient);
            services.AddScoped<IBancoService, BancoService>();
            services.AddScoped<IContactoService, ContactoService>();
            services.AddScoped<IEstadoService, EstadoService>();
            services.AddScoped<IFacturaService, FacturaService>();
            services.AddScoped<IFormaPagoService, FormaPagoService>();
            services.AddScoped<IGestionService, GestionService>();
            services.AddScoped<IObraSocialService, ObraSocialService>();
            services.AddScoped<IPuntoVentaService, PuntoVentaService>();
            services.AddScoped<IReciboService, ReciboService>();
            services.AddScoped<IRespuestaService, RespuestaService>();
            services.AddScoped<ITipoPrestadorService, TipoPrestadorService>();
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            // configure DI for application services
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IBancoRepository, BancoRepository>();
            services.AddScoped<IContactoRepository, ContactoRepository>();
            services.AddScoped<IEstadoRepository, EstadoRepository>();
            services.AddScoped<IFacturaRepository, FacturaRepository>();
            services.AddScoped<IFormaPagoRepository, FormaPagoRepository>();
            services.AddScoped<IGestionRepository, GestionRepository>();
            services.AddScoped<IObraSocialRepository, ObraSocialRepository>();
            services.AddScoped<IPuntoVentaRepository, PuntoVentaRepository>();
            services.AddScoped<IReciboRepository, ReciboRepository>();
            services.AddScoped<IRespuestaRepository, RespuestaRepository>();
            services.AddScoped<ITipoPrestadorRepository, TipoPrestadorRepository>();
            services.AddScoped<IUserRepository, UsuarioRepository>();
            services.AddAutoMapper(typeof(Startup));
            services.AddAuthentication();
            services.AddCors(o =>
            {
                o.AddPolicy("AllowAll", builder =>
                     builder.AllowAnyOrigin()
                     .AllowAnyMethod()
                     .AllowAnyHeader()
                );
            });
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddControllers().AddJsonOptions(x =>
         x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
            ;
            services.AddSwaggerGen(c =>
            {
                c.OperationFilter<ReApplyOptionalRouteParameterOperationFilter>();
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "My API",
                    Version = "v1"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Inserte aqui el token recibido en la respuesta del login",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                             {
                             Type = ReferenceType.SecurityScheme,
                             Id = "Bearer"
                            }
                        },
                             new string[] { }
                    }
              });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath= Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, APIDbContext context)
        {
       
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "APICobranzas.Api v1"));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
               
            }
            app.UseCors(z=>z
                .SetIsOriginAllowed(origin => true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                );
            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseMiddleware<JwtMiddleware>();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
