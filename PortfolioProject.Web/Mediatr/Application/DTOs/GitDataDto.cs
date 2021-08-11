using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioProject.Web.Mediatr.Application.DTOs
{
    public class GitDataDto
    {
        public int Id { get; set; }
        public string full_name { get; set; }
        public string html_url { get; set; }
    }
}
