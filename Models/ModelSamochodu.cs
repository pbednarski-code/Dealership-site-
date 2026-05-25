using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DealerAutoMVC.Models
{
    public class ModelSamochodu
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Marka")]
        public int? MarkaId { get; set; }

        public Marka? Marka { get; set; }

        [Display(Name = "Model")]
        public string Nazwa { get; set; } = "";

        [Display(Name = "Rok produkcji")]
        public int Rok { get; set; }

        [Display(Name = "Pojemność silnika")]
        public int Pojemnosc { get; set; }

        [Display(Name = "Moc KM")]
        public int HorsePower { get; set; }

        [Display(Name = "Cena")]
        public decimal Cena { get; set; }

        [Display(Name = "Przebieg")]
        public int Przebieg { get; set; }

        [Display(Name = "Kolor")]
        public string Kolor { get; set; } = "";

        [Display(Name = "Czy sprzedany")]
        public bool CzySprzedany { get; set; }

        [NotMapped]
        public string MarkaModel
        {
            get
            {
                return (Marka != null ? Marka.Nazwa : "") + " " + Nazwa;
            }
        }

        public Wyposazenie? Wyposazenie { get; set; }

        public Transakcja? Transakcja { get; set; }
    }
}