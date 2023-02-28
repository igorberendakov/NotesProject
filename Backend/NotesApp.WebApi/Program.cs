using AutoMapper.Extensions.ExpressionMapping;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NotesApp.Domain.Entities;
using NotesApp.Infrastructure.Configurations.Authentication;
using NotesApp.Infrastructure.Extentions;
using NotesApp.Infrastructure.Filters;
using NotesApp.Infrastructure.Validation.Validators;
using NotesApp.Services.Extentions;
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
                    //.AddRouteComponents("api", ODataEdmModelBuilder.GetEdmModel())
                    );
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder => builder
                    .WithOrigins("http://localhost:4200")
                    .AllowCredentials()
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            });

            builder.Services.AddFluentValidationAutoValidation()
                .AddValidatorsFromAssemblyContaining<NoteCreateValidator>();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. 
                    Введите 'Bearer' [пробел] и затем токен в окно ввода ниже.
                    Пример: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
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
            builder.Services.AddCurrentUserInfo();
            builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            builder.Services.AddAutoMapper(options =>
            {
                options.AddExpressionMapping();
            },
            AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer();
            builder.Services.ConfigureOptions<JwtOptionsSetup>();
            builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();
            builder.Services.AddHttpContextAccessor();

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

            app.UseCors();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}