using System.ComponentModel.DataAnnotations;

namespace DealerAutoMVC.Models
{
    public class Marka
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Nazwa marki")]
        public string Nazwa { get; set; } = "";

        [Display(Name = "Kraj pochodzenia")]
        public string KrajPochodzenia { get; set; } = "";

        public ICollection<ModelSamochodu>? ModeleSamochodow { get; set; }
    }
}