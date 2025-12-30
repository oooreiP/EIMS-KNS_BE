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

            try
            {
                // Chạy Handler chính
                var response = await next();
                // Kiểm tra kết quả (FluentResults)
                bool isSuccess = true;
                string description = "Thực hiện thành công";
                // Reflection để check Result.IsFailed nếu dùng FluentResults
                var resultProp = response.GetType().GetProperty("IsFailed");
                if (resultProp != null && (bool)resultProp.GetValue(response))
                {
                    isSuccess = false;
                    description = "Thực hiện thất bại (Lỗi nghiệp vụ)";
                }
                // Chỉ log các Command (thay đổi hệ thống), hạn chế log Query (xem dữ liệu) cho đỡ rác
                if (actionName.EndsWith("Query") == false)
                {
                    await _activityLogger.LogAsync(actionName, description, isSuccess);
                }

                return response;
            }
            catch (Exception ex)
            {
                await _activityLogger.LogAsync(actionName, $"Lỗi hệ thống: {ex.Message}", false);
                throw; 
            }
        }
    }
}
