using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Filters
{
    public class HeaderValidationFilter : IAuthorizationFilter
    {
        private readonly string _headerName;
        private readonly string _headerValue;

        public HeaderValidationFilter(string headerName, string headerValue)
        {
            _headerName = headerName;
            _headerValue = headerValue;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(_headerName, out var headerValues))
            {
                context.Result = new BadRequestResult(); // 400
            }
            else if (!headerValues.Contains(_headerValue))
            {
                context.Result = new UnauthorizedResult(); // 401
            }
        }
    }
}
