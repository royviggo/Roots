using System.ComponentModel.DataAnnotations;

namespace Roots.Domain.Enums
{
    public enum Status
    {
        [Display(Name = "Unknown")]
        Unknown = 0,

        [Display(Name = "Living")]
        Living = 1,

        [Display(Name = "Deceased")]
        Deceased = 2,
    }
}