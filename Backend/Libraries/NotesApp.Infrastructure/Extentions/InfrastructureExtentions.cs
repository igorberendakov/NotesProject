using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NotesApp.Domain.Abstractions;
using NotesApp.Domain.Entities;
using NotesApp.Infrastructure.DbConfiguration;
using NotesApp.Infrastructure.Dtos;
using NotesApp.Infrastructure.Repository;
using NotesApp.Infrastructure.Repository.Abstractions;
using NotesApp.Infrastructure.Validation.Validators;

namespace NotesApp.Infrastructure.Extentions
{
    public static class InfrastructureExtentions
    {
        /// <summary>
        /// Добавление контекста базы данных в коллекцию сервисов.
        /// </summary>
        /// <param name="services">Коллекция сервисов.</param>
        /// <returns>Коллекция сервисов.</returns>
        public static IServiceCollection AddNotesDbContext(this IServiceCollection services, string connectionString) 
        {
            services.AddDbContext<IDbContext, NotesDbContext>(options => options
                .UseNpgsql(connectionString)
                .UseSnakeCaseNamingConvention());

            return services;
        }
        /// <summary>
        /// Добавление репозиториев в коллекцию сервисов.
        /// </summary>
        /// <param name="services">Коллекция сервисов.</param>
        /// <returns>Коллекция сервисов.</returns>
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepository<Note>, GenericRepository<Note>>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<IRepository<Notification>, GenericRepository<Notification>>();

            return services;
        }
    }
}