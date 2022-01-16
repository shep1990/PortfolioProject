using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioProject.Web.Mediatr.Application.DTOs
{
    public class CvInformationDto
    {
        public int Id { get; set; }
        public string Section { get; set; }
        public string Heading { get; set; }
        public string Content { get; set; }
    }
}
