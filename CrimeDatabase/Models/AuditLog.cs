using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrimeDatabase.Models
{
    public class AuditLog
    {
        public int Id { get; set; }
        [DataType(DataType.DateTime)]
        [Display(Name = "Action Date & Time")]
        public DateTime ActionDateTime { get; set; }
        [Display(Name = "Type of action")]
        public required string ActionType { get; set; }
        public int? CrimeEventID { get; set; }
        [ForeignKey("CrimeEventID")]
        public virtual CrimeEvent? CrimeEvent { get; set; }
    }
}
