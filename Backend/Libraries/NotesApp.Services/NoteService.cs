using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Microsoft.Extensions.Logging;
using NotesApp.Domain.Abstractions;
using NotesApp.Domain.Entities;
using NotesApp.Infrastructure.Dtos;
using NotesApp.Services.Abstractions;

namespace NotesApp.Services
{
    public class NoteService : INoteService
    {
        private readonly IRepository<Note> _repository;
        private readonly ILogger<NoteService> _logger;
        private readonly IMapper _mapper;

        public NoteService(IRepository<Note> repository, ILogger<NoteService> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Guid> CreateNoteAsync(NoteCreateDto noteCreateDto, CancellationToken cancellationToken = default)
        {
            return await _repository.CreateAsync(_mapper.Map<Note>(noteCreateDto), cancellationToken);
        }

        public async Task<bool> DeleteNoteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await _repository.GetByIdAsync(id, cancellationToken);

            if (entity == null)
            {
                _logger.LogWarning("Попытка удаления несуществующей заметки");

                return false;
            }

            await _repository.DeleteAsync(entity, cancellationToken);

            return true;
        }

        public IQueryable<NoteViewDto> GetQueryable()
        {
            var entities = _repository.GetQueryable();

            return entities.UseAsDataSource(_mapper.ConfigurationProvider).For<NoteViewDto>();
        }

        public async Task<bool> UpdateNoteAsync(NoteUpdateDto noteUpdateDto, CancellationToken cancellationToken = default)
        {
            var entity = await _repository.GetByIdAsync(noteUpdateDto.Id, cancellationToken);

            if (entity == null)
            {
                _logger.LogWarning("Попытка изменения несуществующей заметки, запрашиваемый идентификатор: {NoteId}.", noteUpdateDto.Id);

                return false;
            }

            return await _repository.UpdateAsync(_mapper.Map<Note>(noteUpdateDto), cancellationToken);
        }
    }
}