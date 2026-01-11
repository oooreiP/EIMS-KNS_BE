using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.Captcha;
using SkiaSharp;

namespace EIMS.Infrastructure.Service
{
    public class CaptchaService : ICaptchaService
    {
        // Các ký tự được phép xuất hiện (tránh số 0, 1, chữ O, I dễ nhầm)
        private const string Chars = "2346789ABCDEFGHJKLMNPQRTUVWXYZ";

        public CaptchaResult GenerateCaptchaImage(int width, int height)
        {
            Random random = new Random();

            // 1. Sinh chuỗi ngẫu nhiên 4 ký tự
            string captchaCode = new string(Enumerable.Repeat(Chars, 4)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            // 2. Tạo ảnh bằng SkiaSharp
            using var surface = SKSurface.Create(new SKImageInfo(width, height));
            var canvas = surface.Canvas;

            // Xóa nền trắng
            canvas.Clear(SKColors.White);

            // Vẽ nhiễu (đường kẻ loạn xạ)
            using var paintNoise = new SKPaint
            {
                Color = SKColors.LightGray,
                StrokeWidth = 1,
                IsAntialias = true
            };
            for (int i = 0; i < 10; i++)
            {
                canvas.DrawLine(random.Next(width), random.Next(height), random.Next(width), random.Next(height), paintNoise);
            }

            // Vẽ chữ
            using var paintText = new SKPaint
            {
                Color = SKColors.Black,
                TextSize = 24, // Cỡ chữ
                Typeface = SKTypeface.FromFamilyName("Arial", SKFontStyle.Bold),
                IsAntialias = true
            };

            // Vẽ từng ký tự lệch nhau 1 chút
            for (int i = 0; i < captchaCode.Length; i++)
            {
                float x = 20 + (i * 20);
                float y = 30 + random.Next(-5, 5); // Lệch lên xuống
                canvas.DrawText(captchaCode[i].ToString(), x, y, paintText);
            }

            // Xuất ra byte array
            using var image = surface.Snapshot();
            using var data = image.Encode(SKEncodedImageFormat.Png, 100);

            return new CaptchaResult
            {
                CaptchaCode = captchaCode,
                CaptchaImageBytes = data.ToArray(),
                Timestamp = DateTime.UtcNow
            };
        }
    }
}