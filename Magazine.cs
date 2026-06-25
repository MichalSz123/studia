using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryApp.Models
{
    public class Magazine : Item
    {
        public int IssueNumber { get; private set; }
        public string Publisher { get; private set; }
        public bool IsReferenceOnly { get; private set; }
        public int LoanDays { get; private set; }

        // Zawartość magazynu: lista książek (Book)
        private readonly List<Book> _books = new();
        public IReadOnlyList<Book> Books => _books.AsReadOnly();

        // Domyślnie magazyn jest tylko do czytelni (nie wypożyczalny)
        public Magazine(string title, int issueNumber, string publisher, bool isReferenceOnly = true, int loanDays = 0) : base(title)
        {
            if (issueNumber <= 0) throw new ArgumentException("Numer wydania musi byc > 0");
            if (string.IsNullOrWhiteSpace(publisher)) throw new ArgumentException("Wydawca nie moze byc pusty");
            if (!isReferenceOnly && loanDays <= 0) throw new ArgumentException("Jeśli magazyn można wypożyczyć, podaj dodatnią liczbę dni wypożyczenia.");

            IssueNumber = issueNumber;
            Publisher = publisher;
            IsReferenceOnly = isReferenceOnly;
            LoanDays = loanDays;
        }

        public override string ItemType() => "Magazyn";

        // Zapobiegaj wypożyczeniu, jeśli magazyn jest tylko do czytelni
        public override bool Checkout()
        {
            if (IsReferenceOnly)
                throw new InvalidOperationException("Magazyn jest tylko do czytania w czytelni i nie moze byc wypozyczony.");
            return base.Checkout();
        }

        // Dodaj książkę do magazynu
        public void AddBookToMagazine(Book book)
        {
            if (book == null) throw new ArgumentNullException(nameof(book));
            if (_books.Any(b => b.Id == book.Id)) throw new InvalidOperationException("Ksiazka juz istnieje w magazynie.");
            _books.Add(book);
        }

        public bool RemoveBookFromMagazine(Guid bookId)
        {
            var b = _books.FirstOrDefault(x => x.Id == bookId);
            if (b == null) return false;
            _books.Remove(b);
            return true;
        }

        public override string ToString()
        {
            var baseStr = base.ToString();
            var extra = $" Issues: {IssueNumber}, Publisher: {Publisher}, Books inside: {_books.Count}";
            if (IsReferenceOnly) extra += ", tylko w czytelni";
            else extra += $", max dni wypozyczenia: {LoanDays}";
            return baseStr + extra;
        }
    }
}
