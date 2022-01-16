using Microsoft.EntityFrameworkCore;
using PortfolioProject.DataAccess.Configuration;
using PortfolioProject.DataAccess.Models;
using System;

namespace PortfolioProject.DataAccess
{
    public class PortfolioProjectDbContext : DbContext
    {
        public PortfolioProjectDbContext(DbContextOptions<PortfolioProjectDbContext> options)
            : base(options)
        { }

        public DbSet<PortfolioEntries> PortfolioEntries { get; set; }
        public DbSet<CVContent> CvContents{ get; set; }
        public DbSet<Sections> Sections { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new PortfolioConfiguration());
            modelBuilder.ApplyConfiguration(new SectionConfiguration());
            modelBuilder.ApplyConfiguration(new CVContentConfiguration());


            modelBuilder.Entity<PortfolioEntries>().HasData(
                new PortfolioEntries
                {
                    Id = 1,
                    Heading = "Portfolio Heading",
                    Description = "Portfolio Description"
                }
            );

            modelBuilder.Entity<Sections>().HasData(
                new Sections
                {
                    Id = 1,
                    Name = "Education",
                },
                new Sections
                {
                    Id = 2,
                    Name = "Experiance"
                }
            );

            modelBuilder.Entity<CVContent>().HasData(
                new CVContent
                {
                    Id = 1,
                    SectionId = 1,
                    Heading = "Schools",
                    Content = "School list"
                },
                new CVContent
                {
                    Id = 2,
                    SectionId = 1,
                    Heading = "College",
                    Content = "College list"
                },
                new CVContent
                {
                    Id = 3,
                    SectionId = 1,
                    Heading = "University",
                    Content = "University list"
                },

                new CVContent
                {
                    Id = 4,
                    SectionId = 2,
                    Heading = "Next",
                    Content = "Next experience"
                },
                new CVContent
                {
                    Id = 5,
                    SectionId = 2,
                    Heading = "Gould Tech Experience",
                    Content = "Gould Tech experience"
                }            
            );
        }
    }
}
