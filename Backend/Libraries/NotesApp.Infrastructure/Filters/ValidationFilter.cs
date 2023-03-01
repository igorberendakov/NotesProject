using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NotesApp.Infrastructure.Validation;

namespace NotesApp.Infrastructure.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errorResponse = new ErrorResponse();
                errorResponse.Errors = context.ModelState
                    .Where(x => x.Value?.Errors.Count > 0)
                    .Select(kvp => (PropertyName: kvp.Key, ErrorMessages: kvp.Value?.Errors.Select(x => x.ErrorMessage)))
                    .SelectMany(pe => pe.ErrorMessages?
                    .Select(er => new ValidationErrorModel
                    {
                        PropertyName = pe.PropertyName,
                        Message = er
                    })!)
                    .ToList();
                context.Result = new BadRequestObjectResult(errorResponse);

                return;
            }

            await next();
        }
    }
}
