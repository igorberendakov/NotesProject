using Microsoft.Extensions.Logging;
using NotesApp.Domain.Abstractions;
using NotesApp.Domain.Entities;
using NotesApp.Infrastructure.Repository.Abstractions;

namespace NotesApp.Infrastructure.Repository
{
    public class TagRepository : GenericRepository<Tag>, ITagRepository
    {
        public TagRepository(IDbContext dbContext, ILogger<GenericRepository<Tag>> logger) : base(dbContext, logger)
        {
        }

        public async Task<bool> AddTagToNoteAsync(Guid noteId, Guid tagId, CancellationToken cancellationToken)
        {
            var noteTags = _dbContext.Set<NoteTag>();
            var entity = noteTags.FirstOrDefault(x => x.TagId == tagId && x.NoteId == noteId);

            if (entity != null)
            {
                _logger.LogWarning("Тэг уже добавлен.");

                return false;
            }

            var noteTag = new NoteTag { NoteId = noteId, TagId = tagId };
            await noteTags.AddAsync(noteTag, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Тэг с идентификатором {TagId} успешно добавлен к заметке с идентификатором {NoteId}", tagId, noteId);

            return true;
        }

        public async Task<bool> RemoveTagFromNoteAsync(Guid noteId, Guid tagId, CancellationToken cancellationToken)
        {
            var noteTags = _dbContext.Set<NoteTag>();
            var entity = noteTags.FirstOrDefault(x => x.TagId == tagId && x.NoteId == noteId);

            if (entity == null)
            {
                _logger.LogWarning("У данной заметки нет привязки к тэгу с идентификатором {TagId}.", tagId);

                return false;
            }

            noteTags.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
