using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioProject.Web.Mediatr.Application.Responses
{
    public class GitRetrievalResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }

    public class GitResponseList<TData> : GitRetrievalResponse
    {
        public List<TData> Data { get; set; }
    }
}
