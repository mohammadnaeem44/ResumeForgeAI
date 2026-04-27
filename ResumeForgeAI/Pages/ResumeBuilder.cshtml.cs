using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ResumeForgeAI.Data;

namespace ResumeForgeAI.Pages
{
    [Authorize]
    [IgnoreAntiforgeryToken(Order = 1001)] // Simplify API calls from JS
    public class ResumeBuilderModel : PageModel
    {
        public string? PreloadJson { get; set; }
        public string ResumeId { get; set; } = string.Empty;

        public void OnGet(string? id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var resume = AppDatabase.GetResumeById(id);
                if (resume != null && resume.UserEmail == User.Identity?.Name)
                {
                    PreloadJson = resume.ResumeJson;
                    ResumeId = id;
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> OnPostGenerateSuggestionsAsync([FromBody] AiRequest req)
        {
            if (string.IsNullOrWhiteSpace(req.JobDescription))
                return BadRequest("Job description is empty.");

            string apiKey = Environment.GetEnvironmentVariable("GROQ_API_KEY") ?? "YOUR_API_KEY_HERE"; // Configure properly using IConfiguration in production
            string prompt = $@"Generate two actionable, achievement-oriented bullet points for a resume based on this job description. Keep them brief, under 25 words each, formatting them as a JSON array of strings: [""bullet 1"", ""bullet 2""]. No markdown tags, no extra text.
Job Description:
{req.JobDescription}";

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

            var payload = new
            {
                model = "llama-3.1-8b-instant",
                messages = new[] { new { role = "user", content = prompt } },
                temperature = 0.5
            };

            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://api.groq.com/openai/v1/chat/completions", content);

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest("Failed to generate suggestions.");
            }

            var jsonResp = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(jsonResp);
            var textResponse = doc.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString() ?? "[]";

            // Clean json markdown
            textResponse = textResponse.Trim();
            if (textResponse.StartsWith("```json")) textResponse = textResponse.Substring(7);
            else if (textResponse.StartsWith("```")) textResponse = textResponse.Substring(3);
            if (textResponse.EndsWith("```")) textResponse = textResponse.Substring(0, textResponse.Length - 3);
            textResponse = textResponse.Trim();

            try {
                var list = JsonSerializer.Deserialize<List<string>>(textResponse);
                return new JsonResult(list);
            } catch {
                return new JsonResult(new List<string> { "Implemented modern cloud architecture to improve scalability.", "Migrated monolithic application to microservices." });
            }
        }

        [HttpPost]
        public IActionResult OnPostSaveAsync([FromBody] SaveRequest req)
        {
            string email = User.Identity?.Name ?? "";
            if (string.IsNullOrEmpty(email)) return Unauthorized();

            var resumeData = new ResumeData
            {
                Id = string.IsNullOrEmpty(req.Id) ? Guid.NewGuid().ToString() : req.Id,
                UserEmail = email,
                Title = string.IsNullOrEmpty(req.Title) ? "Untitled Resume" : req.Title,
                ResumeJson = req.ResumeJson,
                UpdatedAt = DateTime.UtcNow
            };

            AppDatabase.SaveResume(resumeData);
            return new JsonResult(new { success = true, id = resumeData.Id });
        }

        public class AiRequest { public string JobDescription { get; set; } = string.Empty; }
        public class SaveRequest { public string Id { get; set; } = string.Empty; public string Title { get; set; } = string.Empty; public string ResumeJson { get; set; } = string.Empty; }
    }
}