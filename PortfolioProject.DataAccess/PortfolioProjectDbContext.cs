using Microsoft.EntityFrameworkCore;
using PortfolioProject.DataAccess.Models;
using System;

namespace PortfolioProject.DataAccess
{
    public class PortfolioProjectDbContext : DbContext
    {
        public DbSet<Portfolio> PortfolioEntries { get; set; }
        public PortfolioProjectDbContext(DbContextOptions<PortfolioProjectDbContext> options)
            : base(options)
        { }
    }
}
