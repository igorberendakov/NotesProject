using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace NotesApp.Infrastructure.Configurations.Authentication.Models
{
    public class CurrentUser
    {
        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            var idString = httpContextAccessor.HttpContext?.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            if (idString != null)
            {
                UserId = new Guid(idString);
            }

            UserName = httpContextAccessor.HttpContext?.User.Identity?.Name;
        }

        public Guid? UserId { get; init; }
        public string? UserName { get; init; }
    }
}
