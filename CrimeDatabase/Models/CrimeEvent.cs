using System.ComponentModel.DataAnnotations;

namespace CrimeDatabase.Models
{
    public class CrimeEvent
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Crime Date")]
        public DateTime CrimeDate { get; set; }
        [Display(Name = "Area")]
        public required string LocationArea { get; set; }
        [Display(Name = "Town")]
        public required string LocationTown { get; set; }
        [Display(Name = "Victim Name")]
        public required string VictimName { get; set; }
        [Display(Name = "Crime Type")]
        public required CrimeTypeEnum CrimeType { get; set; }
        public string? Notes { get; set; }

        public CrimeEvent() { }
    }
}
