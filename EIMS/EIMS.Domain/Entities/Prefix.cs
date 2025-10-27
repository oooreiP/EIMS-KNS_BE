using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Domain.Entities
{
    public class Prefix
    {
        [Key]
        public int PrefixID { get; set; }
        [Required]
        [StringLength(255)]
        public string PrefixName { get; set; } = string.Empty;

        // --- Navigation Properties ---
        [InverseProperty("Serials")]
        public virtual ICollection<Serial> Serials { get; set; } = new List<Serial>();
    }
}