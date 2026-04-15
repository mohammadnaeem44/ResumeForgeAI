using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace ResumeForgeAI.Data
{
    public class User
    {
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string UniversityCompany { get; set; } = string.Empty;
    }

    public static class AppDatabase
    {
        private static readonly string DbPath = Path.Combine(Directory.GetCurrentDirectory(), "database.json");
        private static readonly object _lock = new object();

        public static List<User> GetUsers()
        {
            lock (_lock)
            {
                if (!File.Exists(DbPath))
                {
                    return new List<User>();
                }
                var json = File.ReadAllText(DbPath);
                return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
            }
        }

        public static void SaveUser(User user)
        {
            lock (_lock)
            {
                var users = GetUsers();
                if (!users.Any(u => u.Email.Equals(user.Email, StringComparison.OrdinalIgnoreCase)))
                {
                    users.Add(user);
                    File.WriteAllText(DbPath, JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true }));
                }
            }
        }

        public static User? GetUserByEmail(string email)
        {
            return GetUsers().FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }
    }
}