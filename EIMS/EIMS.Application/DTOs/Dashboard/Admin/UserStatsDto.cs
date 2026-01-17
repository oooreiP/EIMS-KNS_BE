using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.DTOs.Dashboard.Admin;

namespace EIMS.Application.DTOs.Dashboard
{
    public class UserStatsDto
    {
        public int TotalUsers { get; set; }
        public int TotalCustomers { get; set; }
        public int NewUsersThisMonth { get; set; }
        public List<UserRoleStatDto> UsersByRole { get; set; } = new List<UserRoleStatDto>();
    }
}