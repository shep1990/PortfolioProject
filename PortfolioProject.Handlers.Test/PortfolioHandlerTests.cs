

using FluentAssertions;
using log4net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PortfolioProject.DataAccess;
using PortfolioProject.DataAccess.Models;
using PortfolioProject.Web.Mediatr.Application.Handlers;
using PortfolioProject.Web.Mediatr.Application.Queries;
using PortfolioProject.Web.Mediatr.Application.Responses;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace PortfolioProject.Handlers.Test
{
    [TestClass]
    public class PortfolioHandlerTests
    {
        private Mock<ILogger<PortfolioDetailsHandler>> _loggerMock;
        private PortfolioProjectDbContext _db;

        [TestInitialize]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<PortfolioDetailsHandler>>();
            _db = CreateDb();
        }

        private static PortfolioProjectDbContext CreateDb([CallerMemberName] string memberName = "in-memory")
        {
            var dbOptions = new DbContextOptionsBuilder<PortfolioProjectDbContext>()
                .UseInMemoryDatabase(databaseName: memberName)
                .Options;

            var db = new PortfolioProjectDbContext(dbOptions);
            db.AddRange(GetFakeEntities());
            db.SaveChanges();

            return db;
        }

        private static List<object> GetFakeEntities()
        {
            return new List<object>()
            {
                new PortfolioEntries
                {
                    Id = 1,
                    Heading = "Portfolio Heading",
                    Description = "Portfolio Description"
                }
            };
        }

        [TestMethod]
        public async Task WhenAQueryIsPassedToGetPortfolioDetails_ThenReturnValidResults()
        {
            var handler = new PortfolioDetailsHandler(_db, _loggerMock.Object);
            var query = new PortfolioDetailsQuery();

            var sut = await handler.Handle(query, new CancellationToken());

            sut.Data.Should().Contain(x => x.Heading == "Portfolio Heading" && x.Description == "Portfolio Description");
        }

        [TestMethod]
        public async Task WhenAQueryIsPassedAndNoDataExists_ThenReturnPortfolioListWithACountOfZero()
        {
            var list = _db.PortfolioEntries;
            _db.RemoveRange(list);
            _db.SaveChanges();

            var handler = new PortfolioDetailsHandler(_db, _loggerMock.Object);
            var query = new PortfolioDetailsQuery();

            var sut = await handler.Handle(query, new CancellationToken());

            sut.Data.Count.Should().Be(0);
        }
    }
}
