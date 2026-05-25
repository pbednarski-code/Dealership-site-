using System.ComponentModel.DataAnnotations.Schema;

namespace DealerAutoMVC.Models
{
    [NotMapped]
    public class RaportSprzedazy
    {
        public int TransakcjaId { get; set; }
        public string Klient { get; set; } = "";
        public string Samochod { get; set; } = "";
        public DateTime DataTransakcji { get; set; }
        public decimal CenaSprzedazy { get; set; }
        public string FormaPlatnosci { get; set; } = "";
    }
}