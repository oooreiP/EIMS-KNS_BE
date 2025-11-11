using EIMS.Application.DTOs.VietQRDTO;
using EIMS.Application.DTOs;
using FluentResults;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using EIMS.Application.Commons.Interfaces;

namespace EIMS.Infrastructure.Service
{
    public class VietQrLookupService : IExternalCompanyLookupService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<VietQrLookupService> _logger;

        public VietQrLookupService(HttpClient httpClient, ILogger<VietQrLookupService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<Result<CompanyLookupDto>> LookupByTaxCodeAsync(string taxCode)
        {
            try
            {
                var url = $"https://api.vietqr.io/v2/business/{taxCode}";
                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                    return Result.Fail(new Error($"VietQR API error: {response.StatusCode}")
                        .WithMetadata("ErrorCode", "ExternalLookup.ApiError"));

                var json = await response.Content.ReadAsStringAsync();
                var parsed = JsonSerializer.Deserialize<VietQrResponse>(json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (parsed == null || !parsed.IsSuccess || parsed.Data == null)
                    return Result.Fail(new Error("No data found for this tax code")
                        .WithMetadata("ErrorCode", "ExternalLookup.NotFound"));

                var dto = new CompanyLookupDto
                {
                    TaxCode = parsed.Data.TaxCode,
                    CompanyName = parsed.Data.CompanyName,
                    Address = parsed.Data.Address ?? "Chưa cập nhật",
                    Status = parsed.Data.Status ?? "Không rõ",
                    Source = parsed.Metadata.Source ?? "",
                    UpdatedAt = parsed.Metadata.UpdatedAt
                };

                return Result.Ok(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when calling VietQR API for taxCode {TaxCode}", taxCode);
                return Result.Fail(new Error("VietQR lookup failed: " + ex.Message)
                    .WithMetadata("ErrorCode", "ExternalLookup.Exception"));
            }
        }
    }
}
