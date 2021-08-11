using Microsoft.Extensions.Logging;
using PortfolioProject.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioProject.Web.Mediatr.Application.Handlers
{
    public abstract class BaseHandler
    {
        private readonly PortfolioProjectDbContext _dbContext;
        private readonly ILogger _logger;

        public PortfolioProjectDbContext DbContext { get => _dbContext; }
        public ILogger Logger { get => _logger; }

        public BaseHandler(PortfolioProjectDbContext dbContext, ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
    }
}
