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
        public GitRetrievalHandler(PortfolioProjectDbContext dbContext, ILogger<GitRetrievalHandler> logger, IHttpClientFactory httpClient) : base(dbContext, logger, httpClient)
        {
        }

        public async Task<GitResponseList<GitDataDto>> Handle(GitRetrievalQuery request, CancellationToken cancellationToken)
        {
            try { 
                var portfolioEntriesDto = new List<PortfolioEntriesDto>();

                var httpClient = CreateHttpClient();
                var repo = "Shep1990";
                var baseAddress = httpClient.BaseAddress;
                var response = await httpClient.GetStringAsync($"{baseAddress}users/{repo}/repos");
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

        private HttpClient CreateHttpClient()
        {
            var httpClient = ClientFactory.CreateClient("apiUrl");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.UserAgent.TryParseAdd("request");

            return httpClient;
        }
    }
}
