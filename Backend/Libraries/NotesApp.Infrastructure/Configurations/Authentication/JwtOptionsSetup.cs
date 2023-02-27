using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NotesApp.Infrastructure.Configurations.Authentication.Models;

namespace NotesApp.Infrastructure.Configurations.Authentication
{
    public class JwtOptionsSetup : IConfigureNamedOptions<JwtOptions>
    {
        private const string SECTION = "JWT";
        private readonly IConfiguration _configuration;

        public JwtOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(JwtOptions options)
        {
            _configuration.GetSection(SECTION).Bind(options);
        }

        public void Configure(string? name, JwtOptions options)
        {
            Configure(options);
        }
    }
}
