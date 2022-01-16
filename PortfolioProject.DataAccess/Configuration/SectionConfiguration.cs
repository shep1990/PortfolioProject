using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortfolioProject.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PortfolioProject.DataAccess.Configuration
{
    public class SectionConfiguration : IEntityTypeConfiguration<Sections>
    {
        public const string TableName = "Section";

        public void Configure(EntityTypeBuilder<Sections> builder)
        {
            builder.ToTable(TableName)
                .HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
