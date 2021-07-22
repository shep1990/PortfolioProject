

using FluentAssertions;
using Microsoft.EntityFrameworkCore;
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
                new Portfolio
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
            var db = CreateDb();
            var handler = new PortfolioDetailsHandler(db);
            var query = new PortfolioDetailsQuery();

            var sut = await handler.Handle(query, new CancellationToken());

            sut.Data.Should().Contain(x => x.Heading == "Portfolio Heading" && x.Description == "Portfolio Description");
        }

        [TestMethod]
        public async Task WhenAQueryIsPassedAndNoDataExists_ThenReturnPortfolioListWithACountOfZero()
        {
            var db = CreateDb();
            var list = db.PortfolioEntries;
            db.RemoveRange(list);
            db.SaveChanges();

            var handler = new PortfolioDetailsHandler(db);
            var query = new PortfolioDetailsQuery();

            var sut = await handler.Handle(query, new CancellationToken());

            sut.Data.Count.Should().Be(0);
        }

        [TestMethod]
        public async Task WhenAnExceptionIsThrown_ThenTheExceptionIsHandled()
        {
            var handler = new PortfolioDetailsHandler(null);
            var query = new PortfolioDetailsQuery();

            var sut = await handler.Handle(query, new CancellationToken());

            sut.Success.Should().Be(false);
            sut.Message.Should().Contain("Object reference not set to an instance of an object.");
        }
    }
}
