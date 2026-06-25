using System;
using System.Linq;
using LibraryApp.Models;
using LibraryApp.Services;

Console.WriteLine("Biblioteka");

var library = new Library();

// Przykładowe dane
var book1 = new Book("Wiedzmin", "Andrzej Sapkowski", "978-83-01-001", 500);
var book2 = new Book("Pan Tadeusz", "Adam Mickiewicz", "978-83-02-002", 320);
var mag1 = new Magazine("Focus", 12, "Bauer");

library.AddItem(book1);
library.AddItem(book2);
library.AddItem(mag1);

var user = new User("Jan Kowalski");
library.AddUser(user);

void PrintAvailableItems()
{
    Console.WriteLine("Dostepne przedmioty:");
    var available = library.ListAvailableItems().ToList();
    for (int i = 0; i < available.Count; i++)
    {
        Console.WriteLine($"[{i}] {available[i]}");
    }
    if (!available.Any()) Console.WriteLine("Brak dostepnych przedmiotow.");
}

void PrintAllItems()
{
    Console.WriteLine("Wszystkie przedmioty:");
    var all = library.ListAllItems().ToList();
    for (int i = 0; i < all.Count; i++)
    {
        Console.WriteLine($"[{i}] {all[i]}");
    }
}

void PrintUsers()
{
    Console.WriteLine("Uzytkownicy:");
    var users = library.GetType()
        .GetField("_users", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
        ?.GetValue(library) as System.Collections.Generic.Dictionary<Guid, User>;
    if (users == null || users.Count == 0)
    {
        Console.WriteLine("Brak uzytkownikow.");
        return;
    }
    var list = users.Values.ToList();
    for (int i = 0; i < list.Count; i++)
        Console.WriteLine($"[{i}] {list[i].Name} (Id: {list[i].Id})");
}

while (true)
{
    Console.WriteLine("\nMenu:");
    Console.WriteLine("1 - Lista dostepnych przedmiotow");
    Console.WriteLine("2 - Dodaj ksiazke");
    Console.WriteLine("3 - Dodaj uzytkownika");
    Console.WriteLine("4 - Wypozycz przedmiot");
    Console.WriteLine("5 - Zwroc przedmiot");
    Console.WriteLine("6 - Lista wszystkich przedmiotow");
    Console.WriteLine("7 - Lista uzytkownikow");
    Console.WriteLine("8 - Dodaj magazyn");
    Console.WriteLine("9 - Dodaj ksiazke do magazynu");
    Console.WriteLine("10 - Wyswietl zawartosc magazynu");
    Console.WriteLine("0 - Wyjscie");
    Console.Write("Wybierz opcje: ");
    var choice = Console.ReadLine();

    if (choice == "0") break;

    switch (choice)
    {
        case "1":
            PrintAvailableItems();
            break;
        case "2":
            try
            {
                Console.Write("Tytul: ");
                var title = Console.ReadLine() ?? string.Empty;
                Console.Write("Autor: ");
                var author = Console.ReadLine() ?? string.Empty;
                Console.Write("ISBN: ");
                var isbn = Console.ReadLine() ?? string.Empty;
                Console.Write("Liczba stron: ");
                var pagesStr = Console.ReadLine() ?? "0";
                if (!int.TryParse(pagesStr, out var pages)) pages = 0;
                var book = new Book(title, author, isbn, pages);
                library.AddItem(book);
                Console.WriteLine($"Dodano ksiazke: {book.Title} (Id: {book.Id})");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Blad: {ex.Message}");
            }
            break;
        case "3":
            try
            {
                Console.Write("Imie i nazwisko: ");
                var name = Console.ReadLine() ?? string.Empty;
                var newUser = new User(name);
                library.AddUser(newUser);
                Console.WriteLine($"Dodano uzytkownika: {newUser.Name} (Id: {newUser.Id})");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Blad: {ex.Message}");
            }
            break;
        case "4":
            {
                // wybierz uzytkownika
                var usersDict = library.GetType()
                    .GetField("_users", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                    ?.GetValue(library) as System.Collections.Generic.Dictionary<Guid, User>;
                if (usersDict == null || usersDict.Count == 0)
                {
                    Console.WriteLine("Brak uzytkownikow. Dodaj uzytkownika najpierw.");
                    break;
                }
                var users = usersDict.Values.ToList();
                for (int i = 0; i < users.Count; i++)
                    Console.WriteLine($"[{i}] {users[i].Name}");
                Console.Write("Wybierz uzytkownika (index): ");
                var ui = Console.ReadLine();
                if (!int.TryParse(ui, out var uidx) || uidx < 0 || uidx >= users.Count) { Console.WriteLine("Nieprawidlowy index"); break; }
                var selectedUser = users[uidx];

                // wybierz przedmiot dostepny
                var available = library.ListAvailableItems().ToList();
                if (!available.Any()) { Console.WriteLine("Brak dostepnych przedmiotow."); break; }
                for (int i = 0; i < available.Count; i++)
                    Console.WriteLine($"[{i}] {available[i]}");
                Console.Write("Wybierz przedmiot (index): ");
                var ii = Console.ReadLine();
                if (!int.TryParse(ii, out var idx) || idx < 0 || idx >= available.Count) { Console.WriteLine("Nieprawidlowy index"); break; }
                var itemToBorrow = available[idx];
                try
                {
                    library.BorrowItem(selectedUser.Id, itemToBorrow.Id);
                    Console.WriteLine($"Uzytkownik {selectedUser.Name} wypozyczyl: {itemToBorrow.Title}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Blad: {ex.Message}");
                }

                break;
            }
        case "5":
            {
                // wybierz uzytkownika
                var usersDict = library.GetType()
                    .GetField("_users", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                    ?.GetValue(library) as System.Collections.Generic.Dictionary<Guid, User>;
                if (usersDict == null || usersDict.Count == 0)
                {
                    Console.WriteLine("Brak uzytkownikow.");
                    break;
                }
                var users = usersDict.Values.ToList();
                for (int i = 0; i < users.Count; i++)
                    Console.WriteLine($"[{i}] {users[i].Name}");
                Console.Write("Wybierz uzytkownika (index): ");
                var ui = Console.ReadLine();
                if (!int.TryParse(ui, out var uidx) || uidx < 0 || uidx >= users.Count) { Console.WriteLine("Nieprawidlowy index"); break; }
                var selectedUser = users[uidx];

                var borrowedIds = selectedUser.BorrowedItemIds.ToList();
                if (!borrowedIds.Any()) { Console.WriteLine("Uzytkownik nie ma wypozyczonych przedmiotow."); break; }
                for (int i = 0; i < borrowedIds.Count; i++)
                {
                    var it = library.GetItem(borrowedIds[i]);
                    Console.WriteLine($"[{i}] {it}");
                }
                Console.Write("Wybierz przedmiot do zwrotu (index): ");
                var ri = Console.ReadLine();
                if (!int.TryParse(ri, out var ridx) || ridx < 0 || ridx >= borrowedIds.Count) { Console.WriteLine("Nieprawidlowy index"); break; }
                var itemToReturn = library.GetItem(borrowedIds[ridx]);
                try
                {
                    library.ReturnItem(selectedUser.Id, itemToReturn.Id);
                    Console.WriteLine($"Uzytkownik {selectedUser.Name} zwrocil: {itemToReturn.Title}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Blad: {ex.Message}");
                }
                break;
            }
        case "6":
            PrintAllItems();
            break;
        case "7":
            PrintUsers();
            break;
        case "8":
            try
            {
                Console.Write("Tytul magazynu: ");
                var title = Console.ReadLine() ?? string.Empty;
                Console.Write("Numer wydania: ");
                var issueStr = Console.ReadLine() ?? "0";
                if (!int.TryParse(issueStr, out var issue)) issue = 0;
                Console.Write("Wydawca: ");
                var publisher = Console.ReadLine() ?? string.Empty;
                Console.Write("Czy magazyn ma byc tylko do czytelni? (y/n) [y]: ");
                var ro = Console.ReadLine();
                bool isRef = true;
                if (!string.IsNullOrWhiteSpace(ro) && (ro.Equals("n", StringComparison.OrdinalIgnoreCase) || ro.Equals("no", StringComparison.OrdinalIgnoreCase)))
                    isRef = false;
                int loanDays = 0;
                if (!isRef)
                {
                    Console.Write("Maksymalna liczba dni wypozyczenia: ");
                    var daysStr = Console.ReadLine() ?? "0";
                    if (!int.TryParse(daysStr, out loanDays)) loanDays = 0;
                }
                var mag = new Magazine(title, issue, publisher, isRef, loanDays);
                library.AddItem(mag);
                Console.WriteLine($"Dodano magazyn: {mag.Title} (Id: {mag.Id})");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Blad: {ex.Message}");
            }
            break;
        case "9":
            try
            {
                var magazines = library.GetAllItems().OfType<Magazine>().ToList();
                if (!magazines.Any()) { Console.WriteLine("Brak magazynow. Dodaj magazyn najpierw."); break; }
                for (int i = 0; i < magazines.Count; i++) Console.WriteLine($"[{i}] {magazines[i]}");
                Console.Write("Wybierz magazyn (index): ");
                var mi = Console.ReadLine();
                if (!int.TryParse(mi, out var midx) || midx < 0 || midx >= magazines.Count) { Console.WriteLine("Nieprawidlowy index"); break; }
                var selectedMag = magazines[midx];

                // lista dostepnych ksiazek w bibliotece
                var books = library.GetAllItems().OfType<Book>().ToList();
                if (!books.Any())
                {
                    Console.WriteLine("Brak ksiazek w bibliotece. Mozesz stworzyc nowa ksiazke.");
                    Console.Write("Tytul: ");
                    var btitle = Console.ReadLine() ?? string.Empty;
                    Console.Write("Autor: ");
                    var bauthor = Console.ReadLine() ?? string.Empty;
                    Console.Write("ISBN: ");
                    var bisbn = Console.ReadLine() ?? string.Empty;
                    Console.Write("Liczba stron: ");
                    var pagesStr = Console.ReadLine() ?? "0";
                    if (!int.TryParse(pagesStr, out var pages)) pages = 0;
                    var newBook = new Book(btitle, bauthor, bisbn, pages);
                    library.AddItem(newBook);
                    selectedMag.AddBookToMagazine(newBook);
                    Console.WriteLine($"Dodano ksiazke do magazynu: {newBook.Title}");
                }
                else
                {
                    for (int i = 0; i < books.Count; i++) Console.WriteLine($"[{i}] {books[i]}");
                    Console.Write("Wybierz ksiazke (index) lub n - stworz nowa: ");
                    var bi = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(bi) && bi.Equals("n", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.Write("Tytul: ");
                        var btitle = Console.ReadLine() ?? string.Empty;
                        Console.Write("Autor: ");
                        var bauthor = Console.ReadLine() ?? string.Empty;
                        Console.Write("ISBN: ");
                        var bisbn = Console.ReadLine() ?? string.Empty;
                        Console.Write("Liczba stron: ");
                        var pagesStr = Console.ReadLine() ?? "0";
                        if (!int.TryParse(pagesStr, out var pages)) pages = 0;
                        var newBook = new Book(btitle, bauthor, bisbn, pages);
                        library.AddItem(newBook);
                        selectedMag.AddBookToMagazine(newBook);
                        Console.WriteLine($"Dodano ksiazke do magazynu: {newBook.Title}");
                    }
                    else
                    {
                        if (!int.TryParse(bi, out var bidx) || bidx < 0 || bidx >= books.Count) { Console.WriteLine("Nieprawidlowy index"); break; }
                        var chosen = books[bidx];
                        selectedMag.AddBookToMagazine(chosen);
                        Console.WriteLine($"Dodano ksiazke do magazynu: {chosen.Title}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Blad: {ex.Message}");
            }
            break;
        case "10":
            try
            {
                var magazines = library.GetAllItems().OfType<Magazine>().ToList();
                if (!magazines.Any()) { Console.WriteLine("Brak magazynow."); break; }
                for (int i = 0; i < magazines.Count; i++) Console.WriteLine($"[{i}] {magazines[i]}");
                Console.Write("Wybierz magazyn (index): ");
                var mi = Console.ReadLine();
                if (!int.TryParse(mi, out var midx) || midx < 0 || midx >= magazines.Count) { Console.WriteLine("Nieprawidlowy index"); break; }
                var selectedMag = magazines[midx];
                var booksInside = selectedMag.Books;
                Console.WriteLine($"Zawartosc magazynu: {selectedMag.Title} (Issues: {selectedMag.IssueNumber})");
                if (booksInside == null || booksInside.Count == 0) Console.WriteLine("Brak ksiazek w magazynie.");
                else
                {
                    for (int i = 0; i < booksInside.Count; i++)
                        Console.WriteLine($"[{i}] {booksInside[i]}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Blad: {ex.Message}");
            }
            break;
        default:
            Console.WriteLine("Nieznana opcja");
            break;
    }
}

Console.WriteLine("Koniec programu.");
