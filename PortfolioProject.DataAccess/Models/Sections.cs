using System;
using System.Collections.Generic;
using System.Text;

namespace PortfolioProject.DataAccess.Models
{
    public class Sections
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<CVContent> CVContentList { get; set; }
    }
}
