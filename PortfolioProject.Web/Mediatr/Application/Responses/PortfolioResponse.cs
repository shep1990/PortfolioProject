using PortfolioProject.DataAccess.Models;
using PortfolioProject.Web.Mediatr.Application.DTOs;
using System.Collections.Generic;

namespace PortfolioProject.Web.Mediatr.Application.Responses
{
    public class PortfolioResponse 
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }

    public class PortfolioResponse<TData> : PortfolioResponse
    {
        public TData Data { get; set; }
    }

    public class PortfolioResponseList<TData> : PortfolioResponse
    {
        public List<TData> Data { get; set; }
    }
}
