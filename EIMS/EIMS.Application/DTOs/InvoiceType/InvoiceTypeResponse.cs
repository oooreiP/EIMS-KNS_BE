namespace EIMS.Application.DTOs.InvoiceType
{
    public class InvoiceTypeResponse
    {
        public int InvoiceTypeID { get; set; }
        public string Symbol {get; set; } = string.Empty;
        public string TypeName { get; set; } = string.Empty;
    }
}