using Microsoft.Extensions.DependencyInjection;
using NotesApp.Services.Abstractions;

namespace NotesApp.Services.Extentions
{
    public static class ServicesExtentions
    {
        /// <summary>
        /// Добавление сервисов сущностей в коллекцию сервисов.
        /// </summary>
        /// <param name="services">Коллекция сервисов.</param>
        /// <returns>Коллекция сервисов.</returns>
        public static IServiceCollection AddServices(this IServiceCollection services) 
        {
            services.AddScoped<INoteService, NoteService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IAccountService, AccountService>();

            return services;
        }
    }
}
