Console.WriteLine("Podaj liczbę ocen:");
int liczbaOcen = int.Parse(Console.ReadLine());

double suma = 0;

// Pętla do wprowadzania ocen
for (int i = 0; i < liczbaOcen; i++)
    {
    Console.WriteLine($"Podaj ocenę {i + 1}:");

    if (double.TryParse(Console.ReadLine(), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double ocena))
    {
        suma += ocena;
    }
    else
    {
        Console.WriteLine("Błędny format oceny!");
    }
    }

double srednia = suma / liczbaOcen;

Console.WriteLine($"Średnia: {srednia:F2}");

if (srednia >= 3.0)
{
    Console.WriteLine("Uczeń zdał.");
}
else
{
    Console.WriteLine("Uczeń nie zdał.");
}