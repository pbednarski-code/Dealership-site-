using System.ComponentModel.DataAnnotations;

namespace DealerAutoMVC.Models
{
    public class Transakcja
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Klient")]
        public int? KlientId { get; set; }

        public Klient? Klient { get; set; }

        [Display(Name = "Samochód")]
        public int? ModelSamochoduId { get; set; }

        public ModelSamochodu? ModelSamochodu { get; set; }

        [Display(Name = "Data transakcji")]
        [DataType(DataType.Date)]
        public DateTime DataTransakcji { get; set; }

        [Display(Name = "Cena sprzedaży")]
        public decimal CenaSprzedazy { get; set; }

        [Display(Name = "Forma płatności")]
        public string FormaPlatnosci { get; set; } = "";
    }
}