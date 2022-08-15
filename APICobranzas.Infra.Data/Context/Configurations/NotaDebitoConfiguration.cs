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
    public class NotaDebitoConfiguration : IEntityTypeConfiguration<NotaDebito>
    {
        public void Configure(EntityTypeBuilder<NotaDebito> builder)
        {

            builder
                .Property(z => z.Total)
                .HasPrecision(10, 2);

        }
    }
}
