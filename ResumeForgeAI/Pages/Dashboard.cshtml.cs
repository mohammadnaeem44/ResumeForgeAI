using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ResumeForgeAI.Data;

namespace ResumeForgeAI.Pages
{
    [Authorize]
    public class DashboardModel : PageModel
    {
        public List<ResumeData> RecentResumes { get; set; } = new List<ResumeData>();

        public void OnGet()
        {
            var email = User.Identity?.Name;
            if (!string.IsNullOrEmpty(email))
            {
                RecentResumes = AppDatabase.GetResumesByUser(email);
            }
        }

        public IActionResult OnPostDelete(string id)
        {
            var email = User.Identity?.Name;
            var resume = AppDatabase.GetResumeById(id);
            if (resume != null && resume.UserEmail == email)
            {
                AppDatabase.DeleteResume(id);
            }
            return RedirectToPage();
        }
    }
}