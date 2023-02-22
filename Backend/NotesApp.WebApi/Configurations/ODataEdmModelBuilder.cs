using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using NotesApp.Infrastructure.Dtos;

namespace NotesApp.WebApi.Configurations
{
    internal static class ODataEdmModelBuilder
    {
        internal static IEdmModel GetEdmModel()
        {
            var builder = new ODataConventionModelBuilder();
            builder.EntitySet<TagViewDto>("Tags");
            builder.EntitySet<NoteViewDto>("Notes");
            builder.EntitySet<NotificationViewDto>("Notifications");

            return builder.GetEdmModel();
        }
    }
}
