using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PortfolioProject.DataAccess;
using PortfolioProject.Web.Mediatr.Application.Handlers;
using PortfolioProject.Web.Mediatr.Application.Queries;
using System;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace PortfolioProject.Handlers.Test
{
    [TestClass]
    public class GitRetrievalHandlerTests
    {
        private Mock<ILogger<GitRetrievalHandler>> _loggerMock;
        private Mock<IHttpClientFactory> _httpClientFactory;
        private PortfolioProjectDbContext _db;

        [TestInitialize]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<GitRetrievalHandler>>();
            _httpClientFactory = new Mock<IHttpClientFactory>();
            _db = CreateDb();
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
            var client = new HttpClient();
            client.BaseAddress = new System.Uri("https://api.github.com/");

            _httpClientFactory.Setup(_ => _.CreateClient("apiUrl")).Returns(client);

            var handler = new GitRetrievalHandler(_db, _loggerMock.Object, _httpClientFactory.Object);

            var query = new GitRetrievalQuery();
            var sut = await handler.Handle(query, default);

            sut.Data.Should().NotBeNull();
            sut.Data.Count.Should().BeGreaterThan(0);
        }

        [TestMethod]
        public async Task WhenRequestIsSentForGetReposAndAnErrorIsThrown_ThenTheFailureIsLoggedAndHandled()
        {
            _httpClientFactory.Setup(_ => _.CreateClient("apiUrl")).Throws(new System.Exception("An error occured"));

            var handler = new GitRetrievalHandler(_db, _loggerMock.Object, _httpClientFactory.Object);

            var query = new GitRetrievalQuery();
            var sut = await handler.Handle(query, default);

            sut.Data.Should().BeNull();
            sut.Message.Should().Contain("An error occured");
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
