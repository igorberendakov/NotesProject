using AutoFixture;
using Moq;
using NotesApp.Domain.Entities;
using NotesApp.Infrastructure.Dtos;
using NotesApp.Infrastructure.Repository.Abstractions;
using NotesApp.Services;

namespace NotesApp.Tests
{
    public class TagServiceTests : BaseServiceTests<TagService>
    {
        private readonly Mock<ITagRepository> _repositoryMock;

        public TagServiceTests()
        {
            _repositoryMock = GetMock<ITagRepository>();
        }

        [Fact]
        public void GetQueryableTagsNotEmptyTest_Success()
        {
            //Arrange
            var tags = Fixture.Create<List<Tag>>().AsQueryable();
            _repositoryMock.Setup(x => x.GetQueryable()).Returns(tags);

            //Act
            var result = Target.GetQueryable();

            //Assert
            _repositoryMock.Verify(x => x.GetQueryable(), Times.Once);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.IsAssignableFrom<IQueryable<TagViewDto>>(result);
        }

        [Fact]
        public void GetQueryableTagsEmptyTest_Success()
        {
            //Arrange
            var tags = new List<Tag>().AsQueryable();
            _repositoryMock.Setup(x => x.GetQueryable()).Returns(tags);

            //Act
            var result = Target.GetQueryable();

            //Assert
            _repositoryMock.Verify(x => x.GetQueryable(), Times.Once);
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.IsAssignableFrom<IQueryable<TagViewDto>>(result);
        }

        [Fact]
        public async Task CreateTagTest_Success()
        {
            //Arrange
            var tagCreateDto = Fixture.Create<TagCreateDto>();
            var guid = Guid.NewGuid();
            _repositoryMock.Setup(x => x.CreateAsync(It.IsAny<Tag>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(guid);

            //Act
            var result = await Target.CreateTagAsync(tagCreateDto);

            //Assert
            _repositoryMock.Verify(x => x.CreateAsync(It.IsAny<Tag>(), It.IsAny<CancellationToken>()), Times.Once);
            Assert.Equal(guid, result);
        }

        [Fact]
        public async Task UpdateTagAsyncTest_Success()
        {
            //Arrange
            var tagUpdateDto = Fixture.Create<TagUpdateDto>();
            var entity = Fixture.Build<Tag>().With(x => x.Id, tagUpdateDto.Id).Create();
            _repositoryMock.Setup(x => x.GetByIdAsync(entity.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(entity);

            //Act
            var result = await Target.UpdateTagAsync(tagUpdateDto);

            //Assert
            _repositoryMock.Verify(x => x.GetByIdAsync(entity.Id, It.IsAny<CancellationToken>()), Times.Once);
            _repositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Tag>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task DeleteTagAsyncTest_Success()
        {
            //Arrange
            var guid = Guid.NewGuid();
            var entity = Fixture.Build<Tag>().With(x => x.Id, guid).Create();
            _repositoryMock.Setup(x => x.GetByIdAsync(guid, It.IsAny<CancellationToken>()))
                .ReturnsAsync(entity);

            //Act
            var result = await Target.DeleteTagAsync(guid);

            //Assert
            _repositoryMock.Verify(x => x.DeleteAsync(entity, It.IsAny<CancellationToken>()), Times.Once);
            Assert.True(result);
        }

        [Fact]
        public async Task CreateNoteTagTest_Success()
        {
            //Arrange
            var noteTagDto = Fixture.Create<NoteTagDto>();

            //Act
            var result = await Target.AddTagToNoteAsync(noteTagDto);

            //Assert
            _repositoryMock.Verify(x => x.AddTagToNoteAsync(noteTagDto.NoteId, noteTagDto.TagId, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task DeleteNoteTagTest_Success()
        {
            //Arrange
            var noteTagDto = Fixture.Create<NoteTagDto>();

            //Act
            var result = await Target.RemoveTagFromNoteAsync(noteTagDto);

            //Assert
            _repositoryMock.Verify(x => x.RemoveTagFromNoteAsync(noteTagDto.NoteId, noteTagDto.TagId, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
