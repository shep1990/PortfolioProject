using MediatR;
using Microsoft.AspNetCore.Mvc;
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
        private PortfolioController _ctrl;

        [TestInitialize]
        public void TestInit()
        {
            _mediator = new Mock<IMediator>();
            _ctrl = new PortfolioController(_mediator.Object);
        }

        private void SetupExpectedReultsForPortfolio()
        {
            var resp = new PortfolioResponseList<PortfolioEntriesDto> {
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

            _mediator.Setup(x => x.Send(It.IsAny<PortfolioDetailsQuery>(), It.IsAny<CancellationToken>()))
                .Returns(() => Task.FromResult(resp));
        }

        [TestMethod]
        public async Task WhenPortfolioRecordsAreRequestedAndResultIsValid_ThenResultShouldBeOK()
        {
            SetupExpectedReultsForPortfolio();

            var result = await _ctrl.GetPortfolioItems() as OkObjectResult;

            _mediator.Verify(x => x.Send(It.IsAny<PortfolioDetailsQuery>(), default), Times.Once);
        }
    }
}
