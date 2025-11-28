using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.InvoiceTemplate
{
    // This class maps directly to the JSON you save in the database
    public class TemplateConfig
    {
        public DisplaySettings DisplaySettings { get; set; } = new();
        public CustomerSettings CustomerSettings { get; set; } = new();
        public TableSettings TableSettings { get; set; } = new();
        // Optional: If you want to save colors/fonts later
        public StyleSettings Style { get; set; } = new(); 
    }

    public class DisplaySettings
    {
        public bool ShowLogo { get; set; } = true;
        public bool ShowCompanyName { get; set; } = true;
        public bool ShowTaxCode { get; set; } = true;
        public bool ShowAddress { get; set; } = true;
        public bool ShowPhone { get; set; } = true;
        public bool ShowBankAccount { get; set; } = true;
        public bool ShowSignature { get; set; } = true;
        public bool ShowQrCode { get; set; } = true;
        public bool IsBilingual { get; set; } = false;
    }

    public class CustomerSettings
    {
        public bool ShowName { get; set; } = true; // "Tên khách hàng"
        public bool ShowTaxCode { get; set; } = true;
        public bool ShowAddress { get; set; } = true;
        public bool ShowPhone { get; set; } = true; 
        public bool ShowEmail { get; set; } = true;
        public bool ShowPaymentMethod { get; set; } = true;
    }

    public class TableSettings
    {
        public int MinRows { get; set; } = 5; // "Số dòng trong bảng danh sách"
    }

    public class StyleSettings
    {
        public string ColorTheme { get; set; } = "#004aad"; // Default Blue
        public string FontFamily { get; set; } = "Times New Roman";
    }
}