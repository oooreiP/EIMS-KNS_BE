using EIMS.Domain.Enums;

namespace EIMS.Application.DTOs.Authentication
{
    public class AuthResponse
    {
        public int UserID { get; set; }
        public int? CustomerID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
        public UserAccountStatus Status { get; set; }
        public string? EvidenceStoragePath { get; set; }
        public bool IsPasswordChangeRequired { get; set; }
        public string AccessToken { get; set; }
    }
}