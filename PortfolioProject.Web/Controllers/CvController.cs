using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PortfolioProject.Web.Mediatr.Application.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioProject.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CvController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CvController> _logger;

        public CvController(IMediator mediator, ILogger<CvController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("cvItems")]
        public async Task<IActionResult> GetPortfolioItems()
        {
            _logger.LogInformation("Retrieving CV items");
            try
            {
                var response = await _mediator.Send(new CvInformationQuery());
                if (response.Success)
                    return Ok(response);

                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while retrieving the Portfolio Details: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
