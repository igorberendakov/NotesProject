using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NotesApp.Domain.Abstractions;
using NotesApp.Domain.Entities;
using NotesApp.Infrastructure.DbConfiguration;
using NotesApp.Infrastructure.Repository;

namespace NotesApp.Infrastructure.Extentions
{
    public static class InfrastructureExtentions
    {
        public static IServiceCollection AddNotesDbContext(this IServiceCollection services, string connectionString) 
        {
            services.AddDbContext<IDbContext, NotesDbContext>(options => options
                .UseNpgsql(connectionString)
                .UseSnakeCaseNamingConvention());

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepository<Note>, GenericRepository<Note>>();
            services.AddScoped<IRepository<Tag>, GenericRepository<Tag>>();
            services.AddScoped<IRepository<Notification>, GenericRepository<Notification>>();

            return services;
        }
    }
}
