using System.ComponentModel.DataAnnotations;

namespace DealerAutoMVC.Models
{
    public class Uzytkownik
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Login")]
        public string Login { get; set; } = "";

        [Display(Name = "Hash hasła")]
        public string HasloHash { get; set; } = "";

        [Display(Name = "Token API")]
        public string TokenApi { get; set; } = "";

        [Display(Name = "Administrator")]
        public bool CzyAdmin { get; set; }
    }
}