using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FTS.Precatorio.Api.Controllers;
using FTS.Precatorio.Domain.Notifications;
using FTS.Precatorio.Domain.Tickets;
using FTS.Precatorio.Domain.Tickets.Services;
using FTS.Precatorio.Dto;
using FTS.Precatorio.Infrastructure.Database.SQLServer.Context;
using FTS.Precatorio.Infrastructure.Database.SQLServer.Repository;
using FTS.Precatorio.Infrastructure.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using test.Unity.Configs;
using test.Unity.Mocks;
using Xunit;

namespace test.Unity
{
    public class TicketTest
    {
        private readonly Mock<HttpContext> _contextMock;
        private readonly Mock<DbSet<Ticket>> _dbSetTicketMoq;
        private readonly Mock<FTSPrecatorioContext> _contextFtsMock;
        private readonly Mock<TicketRepository> _repositoryMock;
        private readonly Mock<TicketService> _ticketServiceMock;
        private readonly Mock<IUserToken> _userTokenMock;
        public readonly Mock<DomainNotification> _notificationsMock;

        public TicketTest()
        {
            _contextMock = new Mock<HttpContext>();
            _notificationsMock = new Mock<DomainNotification>();
            _userTokenMock = new Mock<IUserToken>();
            _dbSetTicketMoq = new Mock<DbSet<Ticket>>();
            _contextFtsMock = new Mock<FTSPrecatorioContext>(_userTokenMock.Object);
            _repositoryMock = new Mock<TicketRepository>(_contextFtsMock.Object);
            _ticketServiceMock = new Mock<TicketService>(_repositoryMock.Object);
        }

        [Fact]
        public async Task GetTicket()
        {
            // Arrange
            AssertMockContext();

            var controller = new TicketController(_notificationsMock.Object, _ticketServiceMock.Object);
            controller.ControllerContext.HttpContext = _contextMock.Object;

            // Act
            var actionResult = await controller.Get();

            // Assert
            Assert.Equal((actionResult as OkObjectResult).StatusCode, (int)System.Net.HttpStatusCode.OK);

            var viewResult = Assert.IsType<ReturnContentJson<ICollection<Ticket>>>((actionResult as ObjectResult).Value);
            var model = Assert.IsAssignableFrom<ICollection<Ticket>>(viewResult.Data);
            Assert.Single(model);
            Assert.True(viewResult.Success);
        }

        private void AssertMockContext()
        {
            var buff = TicketMock.CreateBuff().AddTicket("123");
            var query = buff.AsQueryable<Ticket>();

            ConfigDbMock.ConfigContext(_contextFtsMock, query, _dbSetTicketMoq);
        }
    }
}