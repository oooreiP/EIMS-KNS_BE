using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Domain.Entities
{
    public class Role
    {
        [Key]
        public int RoleID { get; set; }
        [Required]
        [StringLength(50)]
        public string RoleName { get; set; } = string.Empty;

        // --- Navigation Properties ---
        [InverseProperty("Role")]
        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}