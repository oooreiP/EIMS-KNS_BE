using System.ComponentModel.DataAnnotations;

namespace EIMS.Application.DTOs.Authentication 
{
    public class RegisterRequest
    {
        [Required, MaxLength(100)]
        public string FullName { get; set; }

        [Required, EmailAddress, MaxLength(255)]
        public string Email { get; set; }

        [Required, MinLength(6)]
        public string Password { get; set; }

        [Phone, MaxLength(20)]
        public string? PhoneNumber { get; set; }
        [Required]
        public string RoleName { get; set; }
        /// <summary>
        /// Required if RoleName is 'Customer'.
        /// </summary>
        public string? CompanyName { get; set; }

        /// <summary>
        /// Required if RoleName is 'Customer'.
        /// </summary>
        public string? TaxCode { get; set; }

        /// <summary>
        /// Required if RoleName is 'Customer'.
        /// </summary>
        public string? Address { get; set; }
    }
}