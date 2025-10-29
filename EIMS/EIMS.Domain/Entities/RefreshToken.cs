using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EIMS.Domain.Entities
{
    public class RefreshToken
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("UserID")]
        public int UserId { get; set; }
        [Required]
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public DateTime Created { get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expires;
        public bool IsActive => !IsExpired;
        //navigation
        public User User { get; set; }
    }
}