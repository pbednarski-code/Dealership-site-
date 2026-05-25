using System.Net;
using System.Text;
using System.Text.Json;

string baseUrl = "http://localhost:5264/api/ApiSamochody";
string login = "admin";
string token = "admin-token-123";

while (true)
{
    Console.WriteLine();
    Console.WriteLine("=== API Client - Dealer Samochodowy ===");
    Console.WriteLine("1. Wyświetl samochody");
    Console.WriteLine("2. Wyświetl samochód po ID");
    Console.WriteLine("3. Dodaj samochód");
    Console.WriteLine("4. Edytuj samochód");
    Console.WriteLine("5. Usuń samochód");
    Console.WriteLine("0. Koniec");
    Console.Write("Wybierz opcję: ");

    string? wybor = Console.ReadLine();

    if (wybor == "1")
    {
        GetSamochody();
    }
    else if (wybor == "2")
    {
        GetSamochodPoId();
    }
    else if (wybor == "3")
    {
        DodajSamochod();
    }
    else if (wybor == "4")
    {
        EdytujSamochod();
    }
    else if (wybor == "5")
    {
        UsunSamochod();
    }
    else if (wybor == "0")
    {
        break;
    }
}

void GetSamochody()
{
    string url = $"{baseUrl}?login={login}&token={token}";

    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
    request.Method = "GET";

    try
    {
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();

        using StreamReader reader = new StreamReader(response.GetResponseStream());
        string result = reader.ReadToEnd();

        Console.WriteLine(result);
    }
    catch (WebException ex)
    {
        WypiszBlad(ex);
    }
}

void GetSamochodPoId()
{
    Console.Write("Podaj ID samochodu: ");
    string? id = Console.ReadLine();

    string url = $"{baseUrl}/{id}?login={login}&token={token}";

    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
    request.Method = "GET";

    try
    {
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();

        using StreamReader reader = new StreamReader(response.GetResponseStream());
        string result = reader.ReadToEnd();

        Console.WriteLine(result);
    }
    catch (WebException ex)
    {
        WypiszBlad(ex);
    }
}

void DodajSamochod()
{
    string url = $"{baseUrl}?login={login}&token={token}";

    Console.Write("MarkaId: ");
    int markaId = int.Parse(Console.ReadLine() ?? "1");

    Console.Write("Model: ");
    string nazwa = Console.ReadLine() ?? "";

    Console.Write("Rok: ");
    int rok = int.Parse(Console.ReadLine() ?? "2020");

    Console.Write("Pojemność: ");
    int pojemnosc = int.Parse(Console.ReadLine() ?? "1994");

    Console.Write("Moc KM: ");
    int horsePower = int.Parse(Console.ReadLine() ?? "150");

    Console.Write("Cena: ");
    decimal cena = decimal.Parse(Console.ReadLine() ?? "100000");

    Console.Write("Przebieg: ");
    int przebieg = int.Parse(Console.ReadLine() ?? "0");

    Console.Write("Kolor: ");
    string kolor = Console.ReadLine() ?? "";

    var samochod = new
    {
        markaId = markaId,
        nazwa = nazwa,
        rok = rok,
        pojemnosc = pojemnosc,
        horsePower = horsePower,
        cena = cena,
        przebieg = przebieg,
        kolor = kolor,
        czySprzedany = false
    };

    string json = JsonSerializer.Serialize(samochod);
    WyslijJson(url, "POST", json);
}

void EdytujSamochod()
{
    Console.Write("Podaj ID samochodu do edycji: ");
    int id = int.Parse(Console.ReadLine() ?? "0");

    string url = $"{baseUrl}/{id}?login={login}&token={token}";

    Console.Write("MarkaId: ");
    int markaId = int.Parse(Console.ReadLine() ?? "1");

    Console.Write("Model: ");
    string nazwa = Console.ReadLine() ?? "";

    Console.Write("Rok: ");
    int rok = int.Parse(Console.ReadLine() ?? "2020");

    Console.Write("Pojemność: ");
    int pojemnosc = int.Parse(Console.ReadLine() ?? "1994");

    Console.Write("Moc KM: ");
    int horsePower = int.Parse(Console.ReadLine() ?? "150");

    Console.Write("Cena: ");
    decimal cena = decimal.Parse(Console.ReadLine() ?? "100000");

    Console.Write("Przebieg: ");
    int przebieg = int.Parse(Console.ReadLine() ?? "0");

    Console.Write("Kolor: ");
    string kolor = Console.ReadLine() ?? "";

    var samochod = new
    {
        id = id,
        markaId = markaId,
        nazwa = nazwa,
        rok = rok,
        pojemnosc = pojemnosc,
        horsePower = horsePower,
        cena = cena,
        przebieg = przebieg,
        kolor = kolor,
        czySprzedany = false
    };

    string json = JsonSerializer.Serialize(samochod);
    WyslijJson(url, "PUT", json);
}

void UsunSamochod()
{
    Console.Write("Podaj ID samochodu do usunięcia: ");
    string? id = Console.ReadLine();

    string url = $"{baseUrl}/{id}?login={login}&token={token}";

    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
    request.Method = "DELETE";

    try
    {
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        Console.WriteLine("Usunięto samochód albo żądanie zakończyło się poprawnie.");
        Console.WriteLine("Status: " + response.StatusCode);
    }
    catch (WebException ex)
    {
        WypiszBlad(ex);
    }
}

void WyslijJson(string url, string metoda, string json)
{
    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
    request.ContentType = "application/json";
    request.Method = metoda;

    byte[] dane = Encoding.UTF8.GetBytes(json);
    request.ContentLength = dane.Length;

    using Stream stream = request.GetRequestStream();
    stream.Write(dane, 0, dane.Length);

    try
    {
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();

        using StreamReader reader = new StreamReader(response.GetResponseStream());
        string result = reader.ReadToEnd();

        Console.WriteLine("Status: " + response.StatusCode);
        Console.WriteLine(result);
    }
    catch (WebException ex)
    {
        WypiszBlad(ex);
    }
}

void WypiszBlad(WebException ex)
{
    if (ex.Response != null)
    {
        using StreamReader reader = new StreamReader(ex.Response.GetResponseStream());
        string result = reader.ReadToEnd();

        Console.WriteLine("Błąd:");
        Console.WriteLine(result);
    }
    else
    {
        Console.WriteLine("Błąd połączenia:");
        Console.WriteLine(ex.Message);
    }
}