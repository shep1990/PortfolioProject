using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PortfolioProject.Web.Controllers;
using PortfolioProject.Web.Mediatr.Application.DTOs;
using PortfolioProject.Web.Mediatr.Application.Queries;
using PortfolioProject.Web.Mediatr.Application.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PortfolioProject.Handlers.Test
{
    [TestClass]
    public class PortfolioControllerTests
    {
        private Mock<IMediator> _mediator;
        private Mock<ILogger<PortfolioController>> _loggerMock;
        private PortfolioController _ctrl;

        [TestInitialize]
        public void TestInit()
        {
            _mediator = new Mock<IMediator>();
            _loggerMock = new Mock<ILogger<PortfolioController>>();
            _ctrl = new PortfolioController(_mediator.Object, _loggerMock.Object);
        }

        private async Task<PortfolioResponseList<PortfolioEntriesDto>> SetupExpectedReultsForPortfolio()
        {
            return new PortfolioResponseList<PortfolioEntriesDto> {
                Success = true,
                Data = new List<PortfolioEntriesDto>
                {
                    new PortfolioEntriesDto
                    {
                        Heading = "Heading",
                        Description = "Description",
                        Id = 1
                    }
                }
            };
        }

        [TestMethod]
        public async Task WhenPortfolioRecordsAreRequestedAndResultIsValid_ThenResultShouldBeOK()
        {
            var resp = await SetupExpectedReultsForPortfolio();

            _mediator.Setup(x => x.Send(It.IsAny<PortfolioDetailsQuery>(), It.IsAny<CancellationToken>()))
                .Returns(() => Task.FromResult(resp));

            var result = await _ctrl.GetPortfolioItems() as OkObjectResult;

            _mediator.Verify(x => x.Send(It.IsAny<PortfolioDetailsQuery>(), default), Times.Once);
        }

        [TestMethod]
        public async Task WhenPortfolioRecordsAreRequestedAndResultIsInvalid_ThenResultShouldThrowError()
        {
            var resp = await SetupExpectedReultsForPortfolio();

            _mediator.Setup(x => x.Send(It.IsAny<PortfolioDetailsQuery>(), It.IsAny<CancellationToken>()))
                .Throws(new Exception("Test Exception"));

            var result = await _ctrl.GetPortfolioItems() as OkObjectResult;

            _loggerMock.Verify(l => l.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()),
              Times.Once);
        }
    }
}
