using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Domain.Enums;

namespace EIMS.Application.DTOs.User
{
    public class UserResponse
    {
        public int UserID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string RoleName { get; set; }
        public bool IsActive { get; set; }
        public UserAccountStatus Status { get; set; }
        public string? EvidenceStoragePath { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}