using AutoFixture;
using Moq;
using NotesApp.Domain.Abstractions;
using NotesApp.Domain.Entities;
using NotesApp.Infrastructure.Dtos;
using NotesApp.Services;

namespace NotesApp.Tests
{
    public class NotificationServiceTests : BaseServiceTests<NotificationService>
    {
        private readonly Mock<IRepository<Notification>> _repositoryMock;

        public NotificationServiceTests()
        {
            _repositoryMock = GetMock<IRepository<Notification>>();
        }

        [Fact]
        public void GetQueryableNotificationsNotEmptyTest_Success()
        {
            //Arrange
            var notifications = Fixture.Create<List<Notification>>().AsQueryable();
            _repositoryMock.Setup(x => x.GetQueryable()).Returns(notifications);

            //Act
            var result = Target.GetQueryable();

            //Assert
            _repositoryMock.Verify(x => x.GetQueryable(), Times.Once);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.IsAssignableFrom<IQueryable<NotificationViewDto>>(result);
        }

        [Fact]
        public void GetQueryableNotificationsEmptyTest_Success()
        {
            //Arrange
            var notifications = new List<Notification>().AsQueryable();
            _repositoryMock.Setup(x => x.GetQueryable()).Returns(notifications);

            //Act
            var result = Target.GetQueryable();

            //Assert
            _repositoryMock.Verify(x => x.GetQueryable(), Times.Once);
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.IsAssignableFrom<IQueryable<NotificationViewDto>>(result);
        }

        [Fact]
        public async Task CreateNotificationTest_Success()
        {
            //Arrange
            var notificationCreateDto = Fixture.Create<NotificationCreateDto>();
            var guid = Guid.NewGuid();
            _repositoryMock.Setup(x => x.CreateAsync(It.IsAny<Notification>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(guid);

            //Act
            var result = await Target.CreateNotificationAsync(notificationCreateDto);

            //Assert
            _repositoryMock.Verify(x => x.CreateAsync(It.IsAny<Notification>(), It.IsAny<CancellationToken>()), Times.Once);
            Assert.Equal(guid, result);
        }

        [Fact]
        public async Task UpdateNotificationAsyncTest_Success()
        {
            //Arrange
            var notificationUpdateDto = Fixture.Create<NotificationUpdateDto>();
            var entity = Fixture.Build<Notification>().With(x => x.Id, notificationUpdateDto.Id).Create();
            _repositoryMock.Setup(x => x.GetByIdAsync(entity.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(entity);

            //Act
            var result = await Target.UpdateNotificationAsync(notificationUpdateDto);

            //Assert
            _repositoryMock.Verify(x => x.GetByIdAsync(entity.Id, It.IsAny<CancellationToken>()), Times.Once);
            _repositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Notification>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task DeleteNotificationAsyncTest_Success()
        {
            //Arrange
            var guid = Guid.NewGuid();
            var entity = Fixture.Build<Notification>().With(x => x.Id, guid).Create();
            _repositoryMock.Setup(x => x.GetByIdAsync(guid, It.IsAny<CancellationToken>()))
                .ReturnsAsync(entity);

            //Act
            var result = await Target.DeleteNotificationAsync(guid);

            //Assert
            _repositoryMock.Verify(x => x.DeleteAsync(entity, It.IsAny<CancellationToken>()), Times.Once);
            Assert.True(result);
        }
    }
}
