Console.WriteLine("Wybierz jednostkę temperatury, z której chcesz konwertować(C lub F):");

// Pobierz jednostkę temperatury od użytkownika
char jednostka = Console.ReadKey().KeyChar;

// Sprawdzenie czy użytkownik podaje poprawną jednostkę i wykonanie odpowiedniej konwersji
if (jednostka == 'C' || jednostka == 'c')
{
    Console.WriteLine("\nPodaj temperaturę w stopniach Celsjusza:");
    double celsjusz = Convert.ToDouble(Console.ReadLine());
    double fahrenheit = (celsjusz * 1.8) + 32;
    Console.WriteLine($"Temperatura w stopniach Fahrenheita: {fahrenheit}");
}
else if(jednostka == 'F' || jednostka == 'f')
{
    Console.WriteLine("\nPodaj temperaturę w stopniach Fahrenheita:");
    double fahrenheit = Convert.ToDouble(Console.ReadLine());
    double celsjusz = (fahrenheit - 32) / 1.8;
    Console.WriteLine($"Temperatura w stopniach Celsjusza: {celsjusz}");
}
else
{
    Console.WriteLine("\nNieprawidłowa jednostka. Proszę wybrać C lub F.");
}
