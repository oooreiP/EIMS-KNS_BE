using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace EIMS.API.Extensions
{
    public static class ResultExtensions
    {
      // Xử lý cho Result có dữ liệu trả về (Result<T>)
        public static IActionResult ToActionResult<T>(this Result<T> result)
        {
            if (result.IsSuccess)
            {
                return new OkObjectResult(result.Value);
            }

            // Nếu thất bại, trả về BadRequest kèm danh sách lỗi
            return new BadRequestObjectResult(new
            {
                Success = false,
                Errors = result.Errors.Select(e => e.Message)
            });
        }

        // Xử lý cho Result không có dữ liệu (Result void)
        public static IActionResult ToActionResult(this Result result)
        {
            if (result.IsSuccess)
            {
                return new OkResult();
            }

            return new BadRequestObjectResult(new
            {
                Success = false,
                Errors = result.Errors.Select(e => e.Message)
            });
        }
    }
}