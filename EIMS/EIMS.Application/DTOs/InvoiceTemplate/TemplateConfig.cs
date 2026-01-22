using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.InvoiceTemplate
{
    // This class maps directly to the JSON you save in the database
    public class TemplateConfig
    {
        [JsonPropertyName("display")]
        public DisplaySettings DisplaySettings { get; set; } = new();

        [JsonPropertyName("customer")]
        public CustomerSettings CustomerSettings { get; set; } = new();

        [JsonPropertyName("table")]
        public TableSettings TableSettings { get; set; } = new();

        [JsonPropertyName("style")]
        public StyleSettings Style { get; set; } = new();

        [JsonPropertyName("backgroundUrl")]
        public string? BackgroundUrl { get; set; }
    }

    public class DisplaySettings
    {
        [JsonPropertyName("showLogo")]
        public bool ShowLogo { get; set; } = true;
        [JsonPropertyName("showCompanyName")]
        public bool ShowCompanyName { get; set; } = true;
        [JsonPropertyName("showTaxCode")]
        public bool ShowTaxCode { get; set; } = true;
        [JsonPropertyName("showAddress")]
        public bool ShowAddress { get; set; } = true;
        [JsonPropertyName("showPhone")]
        public bool ShowPhone { get; set; } = true;
        [JsonPropertyName("showBankAccount")]
        public bool ShowBankAccount { get; set; } = true;
        [JsonPropertyName("showSignature")]
        public bool ShowSignature { get; set; } = true;
        [JsonPropertyName("showQrCode")]
        public bool ShowQrCode { get; set; } = true;
        [JsonPropertyName("isBilingual")]
        public bool IsBilingual { get; set; } = false;
    }

    public class CustomerSettings
    {
        [JsonPropertyName("showName")]
        public bool ShowName { get; set; } = true;
        [JsonPropertyName("showTaxCode")]
        public bool ShowTaxCode { get; set; } = true;
        [JsonPropertyName("showAddress")]
        public bool ShowAddress { get; set; } = true;
        [JsonPropertyName("showPhone")]
        public bool ShowPhone { get; set; } = true;
        [JsonPropertyName("showEmail")]
        public bool ShowEmail { get; set; } = true;
        [JsonPropertyName("showPaymentMethod")]
        public bool ShowPaymentMethod { get; set; } = true;
    }

    public class TableSettings
    {
        [JsonPropertyName("minRows")]
        public int MinRows { get; set; } = 5; // "Số dòng trong bảng danh sách"
    }

    public class StyleSettings
    {
        [JsonPropertyName("colorTheme")]
        public string ColorTheme { get; set; } = "#004aad";
        [JsonPropertyName("fontFamily")]
        public string FontFamily { get; set; } = "Times New Roman";
    }
}