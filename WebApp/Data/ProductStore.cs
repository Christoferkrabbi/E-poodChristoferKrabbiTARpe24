using System.Collections.Generic;
using System.Linq;
using WebApp.Models;

namespace WebApp.Data
{
    public static class ProductStore
    {
        private static readonly List<Product> _products = new()
        {
            new Product { Id = 1, Name = "Sword", Price = 50 },
            new Product { Id = 2, Name = "Shield", Price = 35 },
            new Product { Id = 3, Name = "Potion", Price = 10 }
        };

        private static int NextId => (_products.LastOrDefault()?.Id ?? 0) + 1;

        public static IEnumerable<Product> GetAll() => _products;

        public static Product? Find(int id) => _products.FirstOrDefault(p => p.Id == id);

        public static void Add(Product product)
        {
            product.Id = NextId;
            _products.Add(product);
        }

        public static void Update(Product updated)
        {
            var existing = Find(updated.Id);
            if (existing == null) return;
            existing.Name = updated.Name;
            existing.Price = updated.Price;
        }

        public static void Remove(int id)
        {
            var p = Find(id);
            if (p != null) _products.Remove(p);
        }
    }
}