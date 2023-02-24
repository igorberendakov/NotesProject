using NotesApp.Domain.Abstractions;
using NotesApp.Domain.Entities;

namespace NotesApp.Infrastructure.Repository.Abstractions
{
    public interface ITagRepository : IRepository<Tag>
    {
        Task<bool> RemoveTagFromNoteAsync(Guid noteId, Guid tagId, CancellationToken cancellationToken);
        Task<bool> AddTagToNoteAsync(Guid noteId, Guid tagId, CancellationToken cancellationToken);
    }
}
