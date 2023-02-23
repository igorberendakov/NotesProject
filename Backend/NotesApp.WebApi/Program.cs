using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NotesApp.Infrastructure.Extentions;
using NotesApp.Infrastructure.Filters;
using NotesApp.Infrastructure.Validation.Validators;
using NotesApp.Services.Extentions;
using NotesApp.WebApi.Configurations;
using System.Reflection;

namespace NotesApp.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>())
                .AddOData(options => options
                    .EnableQueryFeatures()
                    .AddRouteComponents("api", ODataEdmModelBuilder.GetEdmModel()));

            builder.Services.AddFluentValidationAutoValidation()
                .AddValidatorsFromAssemblyContaining<NoteCreateValidator>();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "NotesApp", Version = "v1" });
                var filePath = Path.Combine(AppContext.BaseDirectory, Assembly.GetExecutingAssembly().GetName().Name + ".xml");
                options.IncludeXmlComments(filePath);
                options.SwaggerDoc("ShopApiDocument", new OpenApiInfo()
                {
                    Version = "v1",
                    Title = "Notes",
                    Description = "An ASP.NET Core Web API for managing Notes"
                });
            });
            builder.Services.AddNotesDbContext(builder.Configuration.GetConnectionString("NotesDbConnection")!);
            builder.Services.AddRepositories();
            builder.Services.AddServices();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger(options =>
                {
                    options.RouteTemplate = "api/swagger/{documentname}/swagger.json";
                });
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("v1/swagger.json", "v1");
                    options.RoutePrefix = "api/swagger";
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}