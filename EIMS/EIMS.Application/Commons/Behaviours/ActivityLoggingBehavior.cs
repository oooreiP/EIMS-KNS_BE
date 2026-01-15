using EIMS.Application.Commons.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EIMS.Application.Commons.Behaviours
{
    public class ActivityLoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    {
        private readonly IActivityLogger _activityLogger;

        public ActivityLoggingBehavior(IActivityLogger activityLogger)
        {
            _activityLogger = activityLogger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            // Lấy tên Command (VD: CompleteSigningCommand -> "CompleteSigning")
            var actionName = typeof(TRequest).Name.Replace("Command", "");
            bool isQuery = actionName.EndsWith("Query");
            try
            {
                var response = await next();
                bool isSuccess = true;
                string description = "Thực hiện thành công";
                var resultProp = response.GetType().GetProperty("IsFailed");
                if (resultProp != null && (bool)resultProp.GetValue(response))
                {
                    isSuccess = false;
                    description = "Thực hiện thất bại (Lỗi nghiệp vụ)";
                }
                if (actionName.EndsWith("Query") == false)
                {
                    await _activityLogger.LogAsync(actionName, description, isSuccess);
                }

                return response;
            }
            catch (Exception ex)
            {
                if (!isQuery)
                {
                    await _activityLogger.LogAsync(actionName, $"Lỗi hệ thống: {ex.Message}", false);
                }

                throw;
            }
        }
    }
}
