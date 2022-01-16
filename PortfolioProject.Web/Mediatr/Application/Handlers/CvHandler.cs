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
    public class CvHandler : BaseHandler, IRequestHandler<CvInformationQuery, CvInformationResponseList<CvInformationDto>>
    {
        public CvHandler(PortfolioProjectDbContext dbContext, ILogger<CvHandler> logger) : base(dbContext, logger)
        {
        }

        public async Task<CvInformationResponseList<CvInformationDto>> Handle(CvInformationQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var cvInformation = DbContext.CvContents.Include(x => x.Section);
                var dtoList = new List<CvInformationDto>();

                foreach (var item in cvInformation)
                {
                    dtoList.Add(new CvInformationDto
                    {
                        Id = item.Id,
                        Content = item.Content,
                        Heading = item.Heading,
                        Section = dtoList.Any(x => x.Section == item.Section.Name) ? string.Empty : item.Section.Name
                    });
                }


                return new CvInformationResponseList<CvInformationDto>
                {
                    Success = true,
                    Data = dtoList
                };
            }
            catch(Exception ex)
            {
                Logger.LogError($"An error occurred");

                return new CvInformationResponseList<CvInformationDto>
                {
                    Message = ex.ToString(),
                    Success = false
                };
            }
        }
    }
}
