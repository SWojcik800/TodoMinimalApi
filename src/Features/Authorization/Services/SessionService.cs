using System.Security.Claims;
using TodoMinimalApi.Common.Exceptions;

namespace TodoMinimalApi.Features.Authorization.Services
{
    /// <summary>
    /// Retrieves user claims from JWT token
    /// </summary>
    public class SessionService : ISessionService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public SessionService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        public string GetUserId()
        {
            var id = _contextAccessor.HttpContext.User.Claims
                .First(c => c.Type == "Id").Value;

            if (String.IsNullOrEmpty(id))
                throw new UnauthorizedException("User is not logged in");

            return id;
        }
    }
}
