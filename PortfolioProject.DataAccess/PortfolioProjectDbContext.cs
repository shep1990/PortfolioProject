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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new PortfolioConfiguration());


            modelBuilder.Entity<PortfolioEntries>().HasData(
                new PortfolioEntries
                {
                    Id = 1,
                    Heading = "Portfolio Heading",
                    Description = "Portfolio Description"
                }
            );
        }
    }
}
