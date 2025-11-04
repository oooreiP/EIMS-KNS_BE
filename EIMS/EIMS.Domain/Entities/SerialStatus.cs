using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EIMS.Domain.Entities
{
    public class SerialStatus
    {
        [Key]
        public int SerialStatusID { get; set; }
        [StringLength(1)]
        public string? Symbol { get; set; }
        [StringLength(255)]
        public string? StatusName { get; set; }

        // --- Navigation Properties ---
        [InverseProperty("SerialStatus")]
        public virtual ICollection<Serial> Serials { get; set; } = new List<Serial>();
    }
}