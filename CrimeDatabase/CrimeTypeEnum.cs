using System.ComponentModel.DataAnnotations;

namespace CrimeDatabase
{
    // a simple enum is used for the crime type options
    // this creates a single place to maintain an update
    // the list, however if this were a frequent requirement
    // these options would be better stored in a database table
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
