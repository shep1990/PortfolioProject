using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortfolioProject.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PortfolioProject.DataAccess.Configuration
{
    public class PortfolioConfiguration : IEntityTypeConfiguration<PortfolioEntries>
    {
        public const string TableName = "PortfolioEntries";

        public void Configure(EntityTypeBuilder<PortfolioEntries> builder)
        {
            builder.ToTable(TableName)
                .HasKey(x => x.Id);

            builder.Property(x => x.Heading)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Description)
                .IsRequired();
        }
    }
}
