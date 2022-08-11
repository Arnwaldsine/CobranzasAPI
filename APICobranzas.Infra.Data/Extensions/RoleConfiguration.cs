using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APICobranzas.Infra.Data.Extensions
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        void IEntityTypeConfiguration<IdentityRole>.Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(new IdentityRole{
                Name ="Usuario",
                NormalizedName ="USUARIO"
            },
            new IdentityRole
            {
                Name = "Administrador",
                NormalizedName= "ADMINISTRADOR"
            }           
            );
            
        }
    }
}
