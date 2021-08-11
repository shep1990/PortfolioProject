using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PortfolioProject.DataAccess;
using PortfolioProject.Web.Mediatr.Application.DTOs;
using PortfolioProject.Web.Mediatr.Application.Queries;
using PortfolioProject.Web.Mediatr.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace PortfolioProject.Web.Mediatr.Application.Handlers
{
    public class GitRetrievalHandler : BaseHandler, IRequestHandler<GitRetrievalQuery, GitResponseList<GitDataDto>>
    {
        public GitRetrievalHandler(PortfolioProjectDbContext dbContext, ILogger logger) : base(dbContext, logger)
        {
        }

        public async Task<GitResponseList<GitDataDto>> Handle(GitRetrievalQuery request, CancellationToken cancellationToken)
        {
            try { 
                var portfolioEntriesDto = new List<PortfolioEntriesDto>();

                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.UserAgent.TryParseAdd("request");
                var repo = "Shep1990";
                var response = httpClient.GetStringAsync($"https://api.github.com/users/{repo}/repos").Result;
                var result = JsonConvert.DeserializeObject<List<GitDataDto>>(response);

                return new GitResponseList<GitDataDto>
                {
                    Success = true,
                    Data = result
                };
            }
            catch(Exception ex)
            {
                Logger.LogError($"An error occurred");

                return new GitResponseList<GitDataDto>
                {
                    Message = ex.ToString(),
                    Success = false
                };
            }
        }
    }
}
