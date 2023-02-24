using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using NotesApp.Infrastructure.Dtos;

namespace NotesApp.WebApi.Configurations
{
    internal static class ODataEdmModelBuilder
    {
        internal static IEdmModel GetEdmModel()
        {
            var builder = new ODataModelBuilder();
            var note = builder.EntityType<NoteViewDto>();
            note.HasKey(x => x.Id);
            note.Property(x => x.Title);
            note.Property(x => x.Text);
            note.HasOptional(x => x.Tags).AutomaticallyExpand(true);
            builder.EntitySet<NoteViewDto>("Notes");

            var tag = builder.EntityType<TagViewDto>();
            tag.HasKey(x => x.Id);
            tag.Property(x => x.Text);
            builder.EntitySet<TagViewDto>("Tags");

            var notification = builder.EntityType<NotificationViewDto>();
            notification.HasKey(x => x.Id);
            notification.Property(x => x.TimeBinding);
            notification.HasRequired(x => x.Note);
            builder.EntitySet<NotificationViewDto>("Notifications");

            return builder.GetEdmModel();
        }
    }
}
