using System.ComponentModel.DataAnnotations;

namespace DealerAutoMVC.Models
{
    public class Wyposazenie
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Samochód")]
        public int? ModelSamochoduId { get; set; }

        public ModelSamochodu? ModelSamochodu { get; set; }

        [Display(Name = "Klimatyzacja")]
        public bool Klimatyzacja { get; set; }

        [Display(Name = "Nawigacja")]
        public bool Nawigacja { get; set; }

        [Display(Name = "Skórzana tapicerka")]
        public bool SkorzanaTapicerka { get; set; }

        [Display(Name = "Kamera cofania")]
        public bool KameraCofania { get; set; }

        [Display(Name = "Czujniki parkowania")]
        public bool CzujnikiParkowania { get; set; }

        [Display(Name = "Apple CarPlay")]
        public bool AppleCarPlay { get; set; }
    }
}