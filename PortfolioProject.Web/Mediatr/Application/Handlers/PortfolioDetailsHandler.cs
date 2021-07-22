using MediatR;
using Microsoft.EntityFrameworkCore;
using PortfolioProject.DataAccess;
using PortfolioProject.Web.Mediatr.Application.DTOs;
using PortfolioProject.Web.Mediatr.Application.Queries;
using PortfolioProject.Web.Mediatr.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PortfolioProject.Web.Mediatr.Application.Handlers
{
    public class PortfolioDetailsHandler : IRequestHandler<PortfolioDetailsQuery, PortfolioResponseList<PortfolioEntriesDto>>
    {
        PortfolioProjectDbContext _dbContext;

        public PortfolioDetailsHandler(PortfolioProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PortfolioResponseList<PortfolioEntriesDto>> Handle(PortfolioDetailsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var portfolioEntriesDto = new List<PortfolioEntriesDto>();

                await _dbContext.PortfolioEntries.ForEachAsync(x =>
                {
                    portfolioEntriesDto.Add(new PortfolioEntriesDto
                    {
                        Id = x.Id,
                        Description = x.Description,
                        Heading = x.Heading
                    });
                });

                return new PortfolioResponseList<PortfolioEntriesDto>
                {
                    Success = true,
                    Data = portfolioEntriesDto
                };
            }
            catch(Exception ex)
            {
                return new PortfolioResponseList<PortfolioEntriesDto>
                {
                    Message = ex.ToString(),
                    Success = false
                };
            }
        }
    }
}
