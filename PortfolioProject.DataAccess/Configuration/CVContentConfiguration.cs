using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortfolioProject.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PortfolioProject.DataAccess.Configuration
{
    public class CVContentConfiguration : IEntityTypeConfiguration<CVContent>
    {
        public const string TableName = "CVContent";

        public void Configure(EntityTypeBuilder<CVContent> builder)
        {
            builder.ToTable(TableName)
                .HasKey(x => x.Id);

            builder.Property(x => x.SectionId)        
                .IsRequired();

            builder.Property(x => x.Heading)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Content);

            builder.HasOne(x => x.Section)
                .WithMany(x => x.CVContentList)
                .HasForeignKey(x => x.SectionId);

        }
    }
}
