using System;
using System.Collections.Generic;
using System.Text;

namespace PortfolioProject.DataAccess.Models
{
    public class CVContent
    {
        public int Id { get; set; }
        public int SectionId { get; set; }
        public string Heading { get; set; }
        public string Content { get; set; }
        public Sections Section { get; set; }
    }
}
