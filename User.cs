using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryApp.Models
{
    public class User
    {
        private Guid _id;
        private string _name;
        private List<Guid> _borrowedItemIds;

        public Guid Id => _id;
        public string Name
        {
            get => _name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Nazwa uzytkownika nie moze byc pusta");
                _name = value;
            }
        }

        public IReadOnlyCollection<Guid> BorrowedItemIds => _borrowedItemIds.AsReadOnly();

        public User(string name)
        {
            _id = Guid.NewGuid();
            Name = name;
            _borrowedItemIds = new List<Guid>();
        }

        public void BorrowItem(Item item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            if (!item.Checkout()) throw new InvalidOperationException("Nie mozna wypozyczyc przedmiotu: brak dostepnosci");
            _borrowedItemIds.Add(item.Id);
        }

        public void ReturnItem(Item item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            if (!_borrowedItemIds.Contains(item.Id)) throw new InvalidOperationException("Uzytkownik nie posiada tego przedmiotu");
            item.Return();
            _borrowedItemIds.Remove(item.Id);
        }
    }
}
