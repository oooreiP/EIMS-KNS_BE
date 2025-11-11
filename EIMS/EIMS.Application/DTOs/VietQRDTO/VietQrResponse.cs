using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.VietQRDTO
{
    public class VietQrResponse
    {
        [JsonPropertyName("code")]
        public string Code { get; set; } = string.Empty;

        [JsonPropertyName("desc")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("data")]
        public VietQrCompanyData Data { get; set; } = new();

        [JsonPropertyName("metadata")]
        public VietQrMetadata Metadata { get; set; } = new();

        public bool IsSuccess => Code == "00";
    }
}
