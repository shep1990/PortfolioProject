using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
    public class PortfolioDetailsHandler : BaseHandler, IRequestHandler<PortfolioDetailsQuery, PortfolioResponseList<PortfolioEntriesDto>>
    {
        public PortfolioDetailsHandler(PortfolioProjectDbContext dbContext, ILogger<PortfolioDetailsHandler> logger) : base(dbContext, logger)
        {
        }

        public async Task<PortfolioResponseList<PortfolioEntriesDto>> Handle(PortfolioDetailsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var portfolioEntriesDto = new List<PortfolioEntriesDto>();

                await DbContext.PortfolioEntries.ForEachAsync(x =>
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
                Logger.LogError($"An error occurred");

                return new PortfolioResponseList<PortfolioEntriesDto>
                {
                    Message = ex.ToString(),
                    Success = false
                };
            }
        }
    }
}
