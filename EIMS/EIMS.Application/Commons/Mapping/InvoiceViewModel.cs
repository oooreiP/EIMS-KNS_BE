using EIMS.Application.DTOs;
using EIMS.Application.DTOs.InvoiceTemplate;

public class InvoiceViewModel
{
    // Template Visuals
    public string FrameUrl { get; set; }
    public string LogoUrl { get; set; }
    public TemplateConfig Config { get; set; } // The JSON object

    // Header Data
    public string Serial { get; set; }
    public string InvoiceNumber { get; set; }
    public int Day { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    
    // Seller
    public string SellerName { get; set; }
    public string SellerTaxCode { get; set; }
    public string SellerAddress { get; set; }
    public string SellerPhone { get; set; }
    public string SellerBankAccount { get; set; }
    
    // Buyer
    public string BuyerName { get; set; }
    public string BuyerCompany { get; set; }
    public string BuyerAddress { get; set; }
    public string BuyerTaxCode { get; set; }
    public string PaymentMethod { get; set; }

    // QR
    public string QrCodeBase64 { get; set; }

    // Table Data
    public List<InvoiceItemDto> Items { get; set; }
    public List<int> FillerRows { get; set; } // Array of empty ints for the loop

    // Totals
    public decimal SubTotal { get; set; }
    public decimal VatRate { get; set; }
    public decimal VatAmount { get; set; }
    public decimal GrandTotal { get; set; } //
    public string AmountInWords { get; set; }
    public string SignDate { get; set; }
}