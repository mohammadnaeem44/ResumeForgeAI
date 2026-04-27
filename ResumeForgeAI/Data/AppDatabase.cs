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

        private static readonly string ResumesDbPath = Path.Combine(Directory.GetCurrentDirectory(), "resumes.json");

        public static List<ResumeData> GetResumes()
        {
            lock (_lock)
            {
                if (!File.Exists(ResumesDbPath)) return new List<ResumeData>();
                var json = File.ReadAllText(ResumesDbPath);
                return JsonSerializer.Deserialize<List<ResumeData>>(json) ?? new List<ResumeData>();
            }
        }

        public static ResumeData? GetResumeById(string id)
        {
            return GetResumes().FirstOrDefault(r => r.Id == id);
        }

        public static List<ResumeData> GetResumesByUser(string email)
        {
            return GetResumes().Where(r => r.UserEmail.Equals(email, StringComparison.OrdinalIgnoreCase)).OrderByDescending(r => r.UpdatedAt).ToList();
        }

        public static void SaveResume(ResumeData resume)
        {
            lock (_lock)
            {
                var resumes = GetResumes();
                var index = resumes.FindIndex(r => r.Id == resume.Id);
                if (index >= 0)
                {
                    resumes[index] = resume; // Update
                }
                else
                {
                    resumes.Add(resume); // Insert
                }
                File.WriteAllText(ResumesDbPath, JsonSerializer.Serialize(resumes, new JsonSerializerOptions { WriteIndented = true }));
            }
        }

        public static void DeleteResume(string id)
        {
            lock (_lock)
            {
                var resumes = GetResumes();
                var index = resumes.FindIndex(r => r.Id == id);
                if (index >= 0)
                {
                    resumes.RemoveAt(index);
                    File.WriteAllText(ResumesDbPath, JsonSerializer.Serialize(resumes, new JsonSerializerOptions { WriteIndented = true }));
                }
            }
        }
    }

    public class ResumeData
    {
        public string Id { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string ResumeJson { get; set; } = string.Empty;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}