using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ResumeForgeAI.Data;

namespace ResumeForgeAI.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public string Name { get; set; } = string.Empty;

        [BindProperty]
        public string Email { get; set; } = string.Empty;

        [BindProperty]
        public string University { get; set; } = string.Empty;

        [BindProperty]
        public string Password { get; set; } = string.Empty;

        public string? ErrorMessage { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var existingUser = AppDatabase.GetUserByEmail(Email);
            if (existingUser != null)
            {
                ErrorMessage = "Email is already registered.";
                return Page();
            }

            var user = new User
            {
                Email = Email,
                Name = Name,
                UniversityCompany = University,
                PasswordHash = HashPassword(Password)
            };

            AppDatabase.SaveUser(user);

            return RedirectToPage("/Login");
        }

        private static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}