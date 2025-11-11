using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.VietQRDTO
{
    public class VietQrMetadata
    {
        [JsonPropertyName("disclaimer")]
        public string? Disclaimer { get; set; }

        [JsonPropertyName("source")]
        public string? Source { get; set; }

        [JsonPropertyName("updatedAt")]
        public DateTime UpdatedAt { get; set; }

        [JsonPropertyName("contact")]
        public string? Contact { get; set; }
    }
}
