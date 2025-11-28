namespace EIMS.Application.DTOs.Invoices
{
    public class InvoiceViewModel
    {
        public string? FrameUrl { get; set; } // Background Image
        public string? LogoUrl { get; set; }
        // Config object to hold your "ShowAddress", "ShowLogo" booleans
        public dynamic Config { get; set; }

        // --- Header Data (From Invoice) ---
        public string Serial { get; set; } = string.Empty;
        public string InvoiceNumber { get; set; } = string.Empty;
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        // --- Seller & Buyer Info ---
        public string SellerName { get; set; } = string.Empty;
        public string SellerTaxCode { get; set; } = string.Empty;
        // ... add other Seller/Buyer fields ...

        // --- Table Data ---
        public List<InvoiceItemDto> Items { get; set; } = new();

        public List<int> FillerRows { get; set; } = new();

        public decimal TotalAmount { get; set; }
        public string AmountInWords { get; set; } = string.Empty;
    }
}