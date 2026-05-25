## Opis projektu

Aplikacja internetowa służy do obsługi oferty dealera samochodowego.  
Umożliwia przeglądanie dostępnych samochodów, marek oraz wyposażenia, a po zalogowaniu pracownika również zarządzanie klientami, transakcjami oraz raportem sprzedaży.

Projekt został wykonany w technologii ASP.NET Core MVC z wykorzystaniem widoków Razor oraz bazy danych SQLite.


Aplikacja wykorzystuje następujące tabele:

- `Marka` — informacje o markach samochodów;
- `ModelSamochodu` — informacje o samochodach znajdujących się w ofercie;
- `Klient` — dane klientów dealera;
- `Transakcja` — informacje o sprzedaży samochodów;
- `Wyposazenie` — informacje o wyposażeniu pojazdów;
- `Uzytkownik` — dane kont pracowników i administratora.

### Relacje pomiędzy tabelami

- jedna `Marka` może posiadać wiele modeli samochodów;
- jeden `Klient` może posiadać wiele transakcji;
- jeden `ModelSamochodu` może być przypisany do transakcji;
- jeden `ModelSamochodu` może posiadać przypisane wyposażenie.


### Funkcjonalności dostępne bez logowania

Użytkownik odwiedzający stronę może:

- przejść na stronę główną;
- przeglądać marki samochodów;
- przeglądać dostępne samochody;
- przeglądać wyposażenie dostępnych samochodów;
- wyświetlać szczegóły ofert.

### Funkcjonalności dostępne po zalogowaniu pracownika

Zalogowany użytkownik może:

- dodawać, edytować i usuwać marki;
- dodawać, edytować i usuwać samochody;
- dodawać, edytować i usuwać wyposażenie;
- zarządzać klientami;
- zarządzać transakcjami sprzedaży;
- wyświetlać raport sprzedaży.

### Funkcjonalności administratora

Administrator może dodatkowo:

- przeglądać użytkowników systemu;
- dodawać nowych użytkowników;
- nadawać użytkownikom token API;
- określać, czy użytkownik jest administratorem.

Podczas pierwszego uruchomienia aplikacji automatycznie tworzony jest użytkownik administratora:
Login: admin
Hasło: admin123
Token API: admin-token-123


### Aby przejść na stronę 
-dotnet build 
-dotnet run

w oknie przeglądarki podać domenę: http://localhost:5264
chyba że tokenem API: http://localhost:5264/api/ApiSamochody?login=admin&token=admin-token-123



