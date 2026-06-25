using System;
using System.Collections.Generic;
using System.Linq;
using LibraryApp.Models;

namespace LibraryApp.Services
{
    public class Library
    {
        private readonly Dictionary<Guid, Item> _items = new();
        private readonly Dictionary<Guid, User> _users = new();

        public void AddItem(Item item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            _items[item.Id] = item;
        }

        public void AddUser(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            _users[user.Id] = user;
        }

        public Item? GetItem(Guid id) => _items.TryGetValue(id, out var it) ? it : null;
        public User? GetUser(Guid id) => _users.TryGetValue(id, out var u) ? u : null;

        public IEnumerable<Item> ListAvailableItems() => _items.Values.Where(i => i.IsAvailable);
        public IEnumerable<Item> ListAllItems() => _items.Values;

        // Nowe metody publiczne do pobierania kolekcji, aby uniknac reflection
        public IEnumerable<User> GetAllUsers() => _users.Values;
        public IEnumerable<Item> GetAllItems() => _items.Values;

        public void BorrowItem(Guid userId, Guid itemId)
        {
            var user = GetUser(userId) ?? throw new InvalidOperationException("Uzytkownik nie znaleziony");
            var item = GetItem(itemId) ?? throw new InvalidOperationException("Przedmiot nie znaleziony");
            user.BorrowItem(item);
        }

        public void ReturnItem(Guid userId, Guid itemId)
        {
            var user = GetUser(userId) ?? throw new InvalidOperationException("Uzytkownik nie znaleziony");
            var item = GetItem(itemId) ?? throw new InvalidOperationException("Przedmiot nie znaleziony");
            user.ReturnItem(item);
        }
    }
}
