using System.ComponentModel.DataAnnotations;

namespace CrimeDatabase
{
    public enum CrimeTypeEnum
    {
        Burglary,
        Robbery,
        Violence,
        [Display(Name="Criminal Damange")]
        CriminalDamage,
        Other
    }
}
