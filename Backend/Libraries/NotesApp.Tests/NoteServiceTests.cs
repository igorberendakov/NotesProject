using AutoFixture;
using Moq;
using NotesApp.Domain.Abstractions;
using NotesApp.Domain.Entities;
using NotesApp.Infrastructure.Dtos;
using NotesApp.Services;

namespace NotesApp.Tests
{
    public class NoteServiceTests : BaseServiceTests<NoteService>
    {
        private readonly Mock<IRepository<Note>> _repositoryMock;

        public NoteServiceTests()
        {
            _repositoryMock = GetMock<IRepository<Note>>();
        }

        [Fact]
        public void GetQueryableNotesNotEmptyTest_Success()
        {
            //Arrange
            var notes = Fixture.Create<List<Note>>().AsQueryable();
            _repositoryMock.Setup(x => x.GetQueryable()).Returns(notes);

            //Act
            var result = Target.GetQueryable();

            //Assert
            _repositoryMock.Verify(x => x.GetQueryable(), Times.Once);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.IsAssignableFrom<IQueryable<NoteViewDto>>(result);
        }

        [Fact]
        public void GetQueryableNotesEmptyTest_Success()
        {
            //Arrange
            var notes = new List<Note>().AsQueryable();
            _repositoryMock.Setup(x => x.GetQueryable()).Returns(notes);

            //Act
            var result = Target.GetQueryable();

            //Assert
            _repositoryMock.Verify(x => x.GetQueryable(), Times.Once);
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.IsAssignableFrom<IQueryable<NoteViewDto>>(result);
        }

        [Fact]
        public async Task CreateNoteTest_Success()
        {
            //Arrange
            var noteCreateDto = Fixture.Create<NoteCreateDto>();
            var guid = Guid.NewGuid();
            _repositoryMock.Setup(x => x.CreateAsync(It.IsAny<Note>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(guid);

            //Act
            var result = await Target.CreateNoteAsync(noteCreateDto);

            //Assert
            _repositoryMock.Verify(x => x.CreateAsync(It.IsAny<Note>(), It.IsAny<CancellationToken>()), Times.Once);
            Assert.Equal(guid, result);
        }

        [Fact]
        public async Task UpdateNoteAsyncTest_Success()
        {
            //Arrange
            var noteUpdateDto = Fixture.Create<NoteUpdateDto>();
            var entity = Fixture.Build<Note>().With(x => x.Id, noteUpdateDto.Id).Create();
            _repositoryMock.Setup(x => x.GetByIdAsync(entity.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(entity);

            //Act
            var result = await Target.UpdateNoteAsync(noteUpdateDto);

            //Assert
            _repositoryMock.Verify(x => x.GetByIdAsync(entity.Id, It.IsAny<CancellationToken>()), Times.Once);
            _repositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Note>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task DeleteNoteAsyncTest_Success()
        {
            //Arrange
            var guid = Guid.NewGuid();
            var entity = Fixture.Build<Note>().With(x => x.Id, guid).Create();
            _repositoryMock.Setup(x => x.GetByIdAsync(guid, It.IsAny<CancellationToken>()))
                .ReturnsAsync(entity);

            //Act
            var result = await Target.DeleteNoteAsync(guid);

            //Assert
            _repositoryMock.Verify(x => x.DeleteAsync(entity, It.IsAny<CancellationToken>()), Times.Once);
            Assert.True(result);
        }
    }
}
