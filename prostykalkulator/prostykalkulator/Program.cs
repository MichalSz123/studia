Console.WriteLine("Prosty kalkulator");

// Pobieranie pierwszej liczby
Console.Write("Podaj pierwszą liczbę: ");
int liczba1 = int.Parse(Console.ReadLine());

// Pobieranie drugiej liczby
Console.Write("Podaj drugą liczbę: ");
int liczba2 = int.Parse(Console.ReadLine());

// Pobieranie operacji
Console.Write("Podaj operację (+, -, *, /): ");
string operacja = Console.ReadLine();

// Obliczanie wyniku na podstawie wybranej operacji
double wynik = 0;

if (operacja == "+")
{
    wynik = liczba1 + liczba2;
}
else if (operacja == "-")
{
    wynik = liczba1 - liczba2;
}
else if (operacja == "*")
{
    wynik = liczba1 * liczba2;
}
else if (operacja == "/")
{
    if (liczba2 != 0)
    {
        wynik = liczba1 / liczba2;
    }
    else
    {
        Console.WriteLine("Błąd: nie można dzielić przez zero!");
        return;
    }
}
else
{
    Console.WriteLine("Błąd: nieznana operacja!");
    return;
}

// Wyświetlanie wyniku
Console.WriteLine($"Wynik: {wynik}");
