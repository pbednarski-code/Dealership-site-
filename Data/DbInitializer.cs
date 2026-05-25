using DealerAutoMVC.Helpers;
using DealerAutoMVC.Models;

namespace DealerAutoMVC.Data
{
    public static class DbInitializer
    {
        public static void Initialize(DealerAutoContext context)
        {
            if (!context.Uzytkownicy.Any())
            {
                Uzytkownik admin = new Uzytkownik
                {
                    Login = "admin",
                    HasloHash = HashHelper.ObliczHash("admin123"),
                    TokenApi = "admin-token-123",
                    CzyAdmin = true
                };

                context.Uzytkownicy.Add(admin);
                context.SaveChanges();
            }
        }
    }
}