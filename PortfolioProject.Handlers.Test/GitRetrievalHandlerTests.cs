using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PortfolioProject.DataAccess;
using PortfolioProject.Web.Mediatr.Application.Handlers;
using PortfolioProject.Web.Mediatr.Application.Queries;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace PortfolioProject.Handlers.Test
{
    [TestClass]
    public class GitRetrievalHandlerTests
    {
        private Mock<ILogger<GitRetrievalHandler>> _loggerMock;

        [TestInitialize]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<GitRetrievalHandler>>();
        }

        private static PortfolioProjectDbContext CreateDb([CallerMemberName] string memberName = "in-memory")
        {
            var dbOptions = new DbContextOptionsBuilder<PortfolioProjectDbContext>()
                .UseInMemoryDatabase(databaseName: memberName)
                .Options;

            var db = new PortfolioProjectDbContext(dbOptions);
            db.SaveChanges();

            return db;
        }

        [TestMethod]
        public async Task WhenRequestIsSentForGetReposAndNewEntriesAreFound_ThenTheseEntriesAreReturned()
        {
            var db = CreateDb();
            var handler = new GitRetrievalHandler(db, _loggerMock.Object);

            var query = new GitRetrievalQuery();
            var sut = await handler.Handle(query, default);

            sut.Data.Count.Should().BeGreaterThan(0);
        }
    }
}
