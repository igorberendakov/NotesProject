using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NotesApp.Domain.Abstractions;
using NotesApp.Infrastructure.DbConfiguration;

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
    }
}
