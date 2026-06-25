using System;

namespace LibraryApp.Models
{
    public class Book : Item
    {
        public string Author { get; private set; }
        public string ISBN { get; private set; }
        public int Pages { get; private set; }

        public Book(string title, string author, string isbn, int pages) : base(title)
        {
            if (string.IsNullOrWhiteSpace(author)) throw new ArgumentException("Author nie może być pusty.");
            if (string.IsNullOrWhiteSpace(isbn)) throw new ArgumentException("ISBN nie może być pusty.");
            if (pages <= 0) throw new ArgumentException("Liczba stron musi być większa od 0.");

            Author = author;
            ISBN = isbn;
            Pages = pages;
        }

        public override string ItemType() => "Ksiazka";
    }
}
