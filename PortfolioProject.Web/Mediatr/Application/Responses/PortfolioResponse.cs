using PortfolioProject.DataAccess.Models;
using PortfolioProject.Web.Mediatr.Application.DTOs;
using System.Collections.Generic;

namespace PortfolioProject.Web.Mediatr.Application.Responses
{
    public class PortfolioResponse 
    {
        public bool Success { get; set; }
        public List<PortfolioEntriesDto> Portfolios { get; set; }
    }
}
