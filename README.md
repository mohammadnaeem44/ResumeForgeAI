<h1 align="center">
  🚀 ResumeForgeAI
</h1>

<p align="center">
  <strong>An AI-powered application built to help job seekers craft the perfect resume and confidently pass Applicant Tracking Systems (ATS).</strong>
</p>

<p align="center">
  <img src="https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet&logoColor=white" alt=".NET 8" />
  <img src="https://img.shields.io/badge/ASP.NET_Core-Razor_Pages-512BD4?logo=dotnet" alt="ASP.NET Core Razor Pages" />
  <img src="https://img.shields.io/badge/OpenAI-GPT--3.5-412991?logo=openai&logoColor=white" alt="OpenAI API" />
  <img src="https://img.shields.io/badge/Tailwind_CSS-38B2AC?logo=tailwind-css&logoColor=white" alt="Tailwind CSS" />
  <img src="https://img.shields.io/badge/PDF_Parsing-PdfPig-red" alt="PdfPig" />
</p>

---

## 🌟 Overview
**ResumeForgeAI** is a comprehensive tool designed for the modern job application process. Navigating the job market often means getting past automated Applicant Tracking Systems (ATS). This project bridges that gap by offering an intuitive **Resume Builder** alongside an **AI-driven ATS Checker** that scores a resume against a specific job description—all running on a modern .NET 8 technical stack.

## ✨ Key Features

- **🤖 AI-Powered ATS Checker:** Upload your existing resume (PDF) and paste a Job Description. The application utilizes the **OpenAI GPT-3.5 API** to deeply analyze your fit, providing:
  - An overall match score.
  - Identified matching skills and missing keywords.
  - Actionable suggestions for improvement.
- **📄 Interactive Resume Builder:** Create professional, aesthetically pleasing resumes with a real-time responsive builder UI. Download the finished resume directly as a PDF.
- **📂 PDF Processing:** Leverages the powerful `UglyToad.PdfPig` library to effortlessly extract text from uploaded PDF resumes for detailed text analysis.
- **🔐 User Authentication:** Built-in membership features allowing users to seamlessly register, log in, and manage their profiles securely.
- **💅 Modern, Responsive UI:** The frontend is beautifully crafted with **Tailwind CSS**, providing a crisp, user-friendly interface that feels modern and professional.

## 💻 Tech Stack

- **Backend:** C#, .NET 8.0, ASP.NET Core Razor Pages
- **Frontend:** HTML5, Razor Syntaxes, Tailwind CSS, FontAwesome
- **AI Integration:** OpenAI API (`gpt-3.5-turbo` via `HttpClient`)
- **PDF Extraction:** `UglyToad.PdfPig`
- **Data Persistence:** Custom lightweight JSON database (with architecture ready to scale with Entity Framework Core & SQL Server)

## 🚀 Getting Started

To get a local copy up and running, follow these simple steps.

### Prerequisites
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- An active [OpenAI API Key](https://platform.openai.com/)
- An IDE such as Visual Studio 2022 or JetBrains Rider

### Installation & Setup

1. **Clone the repository:**
   ```bash
   git clone https://github.com/mohammadnaeem44/ResumeForgeAI.git
   cd ResumeForgeAI
   ```

2. **Configure your API Key:**
   Open the `appsettings.json` file inside the `ResumeForgeAI` folder and add your OpenAI API key:
   ```json
   {
     "Logging": {
       "LogLevel": {
         "Default": "Information",
         "Microsoft.AspNetCore": "Warning"
       }
     },
     "AllowedHosts": "*",
     "OpenAI": {
       "ApiKey": "YOUR_OPENAI_API_KEY_HERE"
     }
   }
   ```
   *(Alternatively, you can configure this securely using .NET User Secrets during development!)*

3. **Build and Run:**
   ```bash
   dotnet run --project ResumeForgeAI
   ```
4. **View in Browser:**
   Open your browser and navigate to `https://localhost:xxxx` (port will be displayed in the terminal).

## 🧑‍💻 Note for Recruiters & Hiring Managers

**ResumeForgeAI** was built to demonstrate proficiency across the full web development stack:
- **Backend Architecture:** Implementing structured Razor Pages, lightweight data management, API integrations, and clean separation of concerns in modern ASP.NET Core.
- **Third-Party Integrations:** Interacting with complex external APIs (OpenAI) to extract structured JSON responses from prompts, showcasing prompt engineering and error handling.
- **File Handling:** Managing file uploads (IFormFile) in memory and securely parsing proprietary formats (PDF text extraction).
- **Frontend Sensibilities:** Designing highly functional and visually appealing user interfaces using utility-first CSS frameworks (Tailwind).

Feel free to browse the source code. The ATS checking logic resides primarily in `Pages/AtsChecker.cshtml.cs`, and the responsive UI structure is best viewed in `Pages/ResumeBuilder.cshtml`.

---

<p align="center">
  <i>Developed with ❤️ by <a href="https://github.com/mohammadnaeem44">Mohammad Naeem</a></i>
</p>
