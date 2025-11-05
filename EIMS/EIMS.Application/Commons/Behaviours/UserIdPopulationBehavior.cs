using EIMS.Application.Commons.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace EIMS.Application.Commons.Behaviors
{
    public class UserIdPopulationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserIdPopulationBehavior(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            // Check if the current command is one that needs authentication
            if (request is IAuthenticatedCommand authenticatedCommand)
            {
                var userIdString = _httpContextAccessor.HttpContext?
                   .User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (!int.TryParse(userIdString, out int userId))
                {
                    throw new ApplicationException("User ID not found in token.");
                }

                authenticatedCommand.AuthenticatedUserId = userId;
            }
            // Continue to the next behavior or the handler
            return await next();
        }

    }
}