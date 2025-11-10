namespace EIMS.Application.DTOs.Serials
{
    public class SerialResponse
    {
        public int SerialID { get; set; }
        public string Serial { get; set; } = string.Empty; // e.g., "1C25TYY"
        public string Description { get; set; } = string.Empty;
    }
}