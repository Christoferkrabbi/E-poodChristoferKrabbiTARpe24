using System.Collections.Generic;
using System.Linq;
using WebApp.Models;

namespace WebApp.Data
{
    public static class UserStore
    {
        private static readonly List<User> _users = new();

        public static IEnumerable<User> Users => _users;

        public static void Add(User user) => _users.Add(user);

        public static User? Find(string username) => _users.FirstOrDefault(u => u.Username == username);
    }
}