using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DealerAutoMVC.Models
{
    public class Klient
    {
        [Key]
        public int Id { get; set; } 

        [Display(Name = "Imię")]
        public string Imie { get; set; } = "";

        [Display(Name = "Nazwisko")]
        public string Nazwisko { get; set; } = "";

        [Display(Name = "Email")]
        public string Email { get; set; } = "";

        [Display(Name = "Telefon")]
        public string Telefon { get; set; } = "";

        [NotMapped]
        public string DaneKlienta
        {
            get
            {
                return Id + " - " + Imie + " " + Nazwisko;
            }
        }

        public ICollection<Transakcja>? Transakcje { get; set; }
    }
}