using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Microsoft.Extensions.Logging;
using NotesApp.Domain.Entities;
using NotesApp.Infrastructure.Dtos;
using NotesApp.Infrastructure.Repository.Abstractions;
using NotesApp.Services.Abstractions;

namespace NotesApp.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _repository;
        private readonly ILogger<TagService> _logger;
        private readonly IMapper _mapper;

        public TagService(ITagRepository repository, ILogger<TagService> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Guid> CreateTagAsync(TagCreateDto tagCreateDto, CancellationToken cancellationToken = default)
        {
            return await _repository.CreateAsync(_mapper.Map<Tag>(tagCreateDto), cancellationToken);
        }

        public async Task<bool> DeleteTagAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await _repository.GetByIdAsync(id, cancellationToken);

            if (entity == null)
            {
                _logger.LogWarning("Попытка удаления несуществующего тэга.");

                return false;
            }

            await _repository.DeleteAsync(entity, cancellationToken);

            return true;
        }

        public IQueryable<TagViewDto> GetQueryable()
        {
            var entities = _repository.GetQueryable();

            return entities.UseAsDataSource(_mapper.ConfigurationProvider).For<TagViewDto>();
        }

        public async Task<bool> AddTagToNoteAsync(NoteTagDto noteTagDto, CancellationToken cancellationToken = default)
        {
            return await _repository.AddTagToNoteAsync(noteTagDto.NoteId, noteTagDto.TagId, cancellationToken);
        }

        public async Task<bool> RemoveTagFromNoteAsync(NoteTagDto noteTagDto, CancellationToken cancellationToken = default)
        {
            return await _repository.RemoveTagFromNoteAsync(noteTagDto.NoteId, noteTagDto.TagId, cancellationToken);
        }

        public async Task<bool> UpdateTagAsync(TagUpdateDto tagUpdateDto, CancellationToken cancellationToken = default)
        {
            var entity = await _repository.GetByIdAsync(tagUpdateDto.Id, cancellationToken);

            if (entity == null)
            {
                _logger.LogWarning("Попытка изменения несуществующего тэга, запрашиваемый идентификатор: {TagId}.", tagUpdateDto.Id);

                return false;
            }

            return await _repository.UpdateAsync(_mapper.Map<Tag>(tagUpdateDto), cancellationToken);
        }
    }
}