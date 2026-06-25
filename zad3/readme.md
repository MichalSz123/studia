# Projekt: Biblioteka (model obiektowy)

## Opis tematu
Aplikacja konsolowa modelująca system biblioteczny. Pozwala zarządzać książkami, magazynami, użytkownikami oraz wypożyczeniami. Umożliwia dodawanie książek do biblioteki i magazynów, wypożyczanie i zwracanie zasobów oraz przeglądanie zawartości magazynów.

## Lista klas
- **Item** (abstrakcyjna) – baza dla zasobów biblioteki. Właściwości: `Id`, `Title`, `IsAvailable`. Metody: `Checkout()`, `Return()`, `ItemType()`.
- **Book** : Item – reprezentuje książkę. Właściwości: `Author`, `ISBN`, `Pages`.
- **Magazine** : Item – reprezentuje magazyn. Właściwości: `IssueNumber`, `Publisher`, `IsReferenceOnly`, `LoanDays`, kolekcja książek (`Books`). Metody: `AddBookToMagazine()`, `RemoveBookFromMagazine()`.
- **User** – reprezentuje użytkownika. Właściwości: `Id`, `Name`, `BorrowedItemIds`. Metody: `BorrowItem()`, `ReturnItem()`.
- **Library** – zarządza kolekcją zasobów i użytkowników. Metody: `AddItem()`, `AddUser()`, `BorrowItem()`, `ReturnItem()`, `ListAvailableItems()`, `ListAllItems()`, `GetAllUsers()`, `GetAllItems()`.

## Relacje między klasami
- **Agregacja**: Library przechowuje kolekcje Item i User (słowniki).
- **Kompozycja**: Magazine przechowuje kolekcję Book (książki w magazynie).
- **Asocjacja**: User przechowuje identyfikatory wypożyczonych zasobów.
- **Dziedziczenie**: Book i Magazine dziedziczą po Item.
- **Parametry/metody**: Operacje wypożyczeń i zwrotów przyjmują identyfikatory użytkownika i zasobu.

## Menu konsolowe
  - Dodawanie książek do biblioteki
  - Dodawanie użytkowników
  - Dodawanie magazynów
  - Dodawanie książek do magazynów (wybór istniejącej lub utworzenie nowej)
  - Wyświetlanie zawartości magazynu (lista książek w wybranym magazynie)
  - Wypożyczanie i zwracanie zasobów
  - Przeglądanie listy dostępnych i wszystkich zasobów oraz użytkowników

## Zastosowanie zasad OOP
- **Enkapsulacja**: prywatne pola, publiczne właściwości z walidacją (np. `Title`, `Name`), kontrolowany dostęp do stanu (`IsAvailable` tylko do odczytu).
- **Dziedziczenie**: Item → Book, Magazine.
- **Polimorfizm**: wywołanie `ItemType()` i `ToString()` na różnych typach Item zwraca różne wyniki.
- **Abstrakcja**: Item jest klasą abstrakcyjną z abstrakcyjną metodą `ItemType()`.