using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.VietQRDTO
{
    public class VietQrCompanyData
    {
        [JsonPropertyName("id")]
        public string TaxCode { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string CompanyName { get; set; } = string.Empty;

        [JsonPropertyName("internationalName")]
        public string? InternationalName { get; set; }

        [JsonPropertyName("shortName")]
        public string? ShortName { get; set; }

        [JsonPropertyName("address")]
        public string? Address { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }
    }

}
