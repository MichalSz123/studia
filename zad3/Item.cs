using System;

namespace LibraryApp.Models
{
    // Abstrakcyjna klasa reprezentująca zasób biblioteki
    public abstract class Item
    {
        private Guid _id;
        private string _title;
        private bool _isAvailable;

        public Guid Id => _id;
        public string Title
        {
            get => _title;
            private set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Tytuł nie może być pusty.");
                _title = value;
            }
        }
        public bool IsAvailable
        {
            get => _isAvailable;
            protected set => _isAvailable = value;
        }

        protected Item(string title)
        {
            _id = Guid.NewGuid();
            Title = title;
            _isAvailable = true;
        }

        public virtual bool Checkout()
        {
            if (!IsAvailable) return false;
            IsAvailable = false;
            return true;
        }

        public virtual void Return()
        {
            IsAvailable = true;
        }

        public abstract string ItemType();

        public override string ToString()
        {
            return $"[{ItemType()}] {Title} (Id: {Id}) - {(IsAvailable ? "dostępny" : "wypożyczony")}";
        }
    }
}
