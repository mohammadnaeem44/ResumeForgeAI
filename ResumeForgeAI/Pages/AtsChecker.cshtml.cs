using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using UglyToad.PdfPig;

namespace ResumeForgeAI.Pages
{
    public class AtsCheckerModel : PageModel
    {
        private readonly IConfiguration _config;

        public AtsCheckerModel(IConfiguration config)
        {
            _config = config;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAnalyzeAsync(IFormFile? resumeFile, string? resumeText, string jdText)
        {
            if (string.IsNullOrWhiteSpace(jdText))
            {
                return BadRequest("Job description is required.");
            }

            string extractedText = resumeText ?? "";

            if (resumeFile != null && resumeFile.Length > 0)
            {
                if (resumeFile.FileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                {
                    try
                    {
                        using var stream = resumeFile.OpenReadStream();
                        using var document = PdfDocument.Open(stream);
                        var sb = new StringBuilder();
                        foreach (var page in document.GetPages())
                        {
                            sb.Append(page.Text);
                            sb.Append(" ");
                        }
                        extractedText = sb.ToString();
                    }
                    catch (Exception ex)
                    {
                        return BadRequest($"Failed to parse PDF: {ex.Message}");
                    }
                }
                else
                {
                    return BadRequest("Only PDF files or plain text are supported right now.");
                }
            }

            if (string.IsNullOrWhiteSpace(extractedText))
            {
                return BadRequest("No resume content found. Please upload a PDF or paste text.");
            }

            var apiKey = _config["OpenAI:ApiKey"];
            if (string.IsNullOrEmpty(apiKey))
            {
                return StatusCode(500, "API Key is missing from configuration.");
            }

            var responseJson = "";
            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                string promptContent = $@"You are an expert Applicant Tracking System (ATS). Analyze the following resume against the provided job description.
Return ONLY a valid JSON object (no markdown, no extra text) with this exact structure:
{{
  ""score"": 85,
  ""matchedKeywords"": [""C#"", ""React""],
  ""missingKeywords"": [""AWS"", ""Docker""],
  ""suggestions"": [""Add metrics to your experience"", ""Include AWS certificate""]
}}

Resume:
{extractedText}

Job Description:
{jdText}";

                var payload = new
                {
                    model = "gpt-3.5-turbo",
                    messages = new[] { new { role = "user", content = promptContent } },
                    temperature = 0.2
                };

                var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
                var response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);

                if (!response.IsSuccessStatusCode)
                {
                    var err = await response.Content.ReadAsStringAsync();
                    return BadRequest($"OpenAI API Error ({(int)response.StatusCode}): {err}");
                }

                var jsonResp = await response.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(jsonResp);
                var textResponse = doc.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();

                if (string.IsNullOrEmpty(textResponse)) return BadRequest("AI returned an empty response.");

                textResponse = CleanJsonResponse(textResponse);
                var result = JsonSerializer.Deserialize<AtsResult>(textResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        private string CleanJsonResponse(string text)
        {
            text = text.Trim();
            if (text.StartsWith("```json"))
            {
                text = text.Substring(7);
                if (text.EndsWith("```")) text = text.Substring(0, text.Length - 3);
            }
            else if (text.StartsWith("```"))
            {
                text = text.Substring(3);
                if (text.EndsWith("```")) text = text.Substring(0, text.Length - 3);
            }
            return text.Trim();
        }
    }

    public class AtsResult
    {
        public int Score { get; set; }
        public List<string> MatchedKeywords { get; set; } = new List<string>();
        public List<string> MissingKeywords { get; set; } = new List<string>();
        public List<string> Suggestions { get; set; } = new List<string>();
    }
}