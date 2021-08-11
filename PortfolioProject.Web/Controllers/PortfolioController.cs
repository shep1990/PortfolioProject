using log4net;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PortfolioProject.Web.Mediatr.Application.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PortfolioProject.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public PortfolioController(IMediator mediator, ILogger logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        // GET: api/<PortfolioController>
        [HttpGet("items")]
        public async Task<IActionResult> GetPortfolioItems()
        {
            _logger.LogInformation("Retrieving portfolio items");
            try {
                var response = await _mediator.Send(new PortfolioDetailsQuery());

                if (response.Success)
                    return Ok(response);

                return BadRequest(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while retrieving the Portfolio Details: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
