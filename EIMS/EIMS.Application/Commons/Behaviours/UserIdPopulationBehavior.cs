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
            if (request is IAuthenticatedCommand authenticatedCommand)
            {
                var user = _httpContextAccessor.HttpContext?.User;

                if (user != null)
                {
                    // 1. Populate UserID (You likely already have this)
                    var userIdString = _httpContextAccessor.HttpContext?
                    .User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                    if (!int.TryParse(userIdString, out int userId))
                    {
                        throw new ApplicationException("User ID not found in token.");
                    }

                    var customerIdClaim = user.FindFirst("CustomerId");
                    if (customerIdClaim != null && int.TryParse(customerIdClaim.Value, out int customerId))
                    {
                        authenticatedCommand.CustomerId = customerId;
                    }
                }
            }
            return await next();
        }
    }
}