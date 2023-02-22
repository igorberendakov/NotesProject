using Microsoft.Extensions.DependencyInjection;
using NotesApp.Services.Abstractions;

namespace NotesApp.Services.Extentions
{
    public static class ServicesExtentions
    {
        public static IServiceCollection AddServices(this IServiceCollection services) 
        {
            services.AddScoped<INoteService, NoteService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<INotificationService, NotificationService>();

            return services;
        }
    }
}
