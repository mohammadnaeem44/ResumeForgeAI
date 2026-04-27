<h1 align="center">
  🚀 ResumeForgeAI
</h1>

<p align="center">
  <strong>An AI-powered application built to help job seekers craft the perfect resume and confidently pass Applicant Tracking Systems (ATS).</strong>
</p>

<p align="center">
  <img src="https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet&logoColor=white" alt=".NET 8" />
  <img src="https://img.shields.io/badge/ASP.NET_Core-Razor_Pages-512BD4?logo=dotnet" alt="ASP.NET Core Razor Pages" />
  <img src="https://img.shields.io/badge/Groq-Llama_3.1--8B-412991?logo=groq&logoColor=white" alt="Groq Llama 3.1 API" />
  <img src="https://img.shields.io/badge/Tailwind_CSS-38B2AC?logo=tailwind-css&logoColor=white" alt="Tailwind CSS" />
  <img src="https://img.shields.io/badge/PDF_Parsing-PdfPig-red" alt="PdfPig" />
</p>

---

## 🌟 Overview
**ResumeForgeAI** is a comprehensive tool designed for the modern job application process. Navigating the job market often means getting past automated Applicant Tracking Systems (ATS). This project bridges that gap by offering an intuitive **Resume Builder** alongside an **AI-driven ATS Checker** that scores a resume against a specific job description—all running on a modern .NET 8 technical stack utilizing the lightning-fast **Groq API with Llama 3.1 8B**.

## ✨ Key Features

- **🤖 AI-Powered ATS Checker & Assistant:** Upload your existing resume (PDF) and paste a Job Description. The application utilizes the **Groq Llama 3.1 API** to deeply analyze your fit, providing:
  - An overall match score and dynamic generative feedback.
  - Identified matching skills and missing keywords.
  - Actionable suggestions for improvement directly in the builder.
- **📄 Interactive Resume Builder with Multiple Themes:** Create professional, aesthetically pleasing resumes with a real-time responsive builder UI. 
  - Switch between 5 dynamically loading visual templates (`Modern`, `Classic`, `Creative`, `Minimal`, and `Tech`).
  - Flexibly duplicate data-entry fields using deep-cloned inputs that map identically to your preview document.
  - Download the finished resume directly as an A4-optimized crisp PDF without any default browser margins!
- **🕒 Real-time Local State Persistence:** JSON-based state saving tracks your resume history natively into your Custom Dashboard, syncing title, template, and all bullet points allowing quick edits and downloads of past iterations.
- **📂 PDF Processing:** Leverages the powerful `UglyToad.PdfPig` library to effortlessly extract text from uploaded PDF resumes for detailed text analysis.
- **🔐 User Authentication:** Built-in membership features allowing users to seamlessly register, log in, and manage their profiles securely.
- **💅 Modern, Responsive UI:** The frontend is beautifully crafted with **Tailwind CSS**, providing a crisp, user-friendly interface that feels modern and professional.

## 📸 Application Gallery

<details>
<summary>Click to view screenshots of ResumeForgeAI in action</summary>

### 1. Interactive Resume Builder
*Features a side-by-side split screen for real-time live preview editing and an AI Assistant to generate achievement bullets.*
<img src="ResumeForgeAI/assets/Resume%20details.png" alt="Resume Builder Details" width="100%">

### 2. Deep ATS Score Analysis
*Get instantaneous feedback on your current resume relative to a target job description.*
<img src="ResumeForgeAI/assets/ats%20score.png" alt="ATS Score Summary" width="100%">

### 3. Cover Letter Workflows
*Easily pivot from resume building directly to crafting the perfect matching cover letter.*
<img src="ResumeForgeAI/assets/cover%20letter.png" alt="Cover Letter Management" width="100%">

</details>

## 💻 Tech Stack

- **Backend:** C#, .NET 8.0, ASP.NET Core Razor Pages
- **Frontend:** HTML5, Razor Syntaxes, Tailwind CSS, Vanilla JS
- **AI Integration:** Groq API (`llama-3.1-8b-instant` via `HttpClient`)
- **PDF Extraction:** `UglyToad.PdfPig`
- **Data Persistence:** Custom lightweight JSON database mapping for auth and dynamically loaded resume models.

## 🚀 Getting Started

To get a local copy up and running, follow these simple steps.

### Prerequisites
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- An active [Groq API Key](https://console.groq.com/) (The app provides a fallback testing key for Llama 3.1 inference by default within `AtsChecker.cshtml.cs` & `ResumeBuilder.cshtml.cs`).
- An IDE such as Visual Studio 2022 or JetBrains Rider

### Installation & Setup

1. **Clone the repository:**
   ```bash
   git clone https://github.com/mohammadnaeem44/ResumeForgeAI.git
   cd ResumeForgeAI
   ```

2. **Configure your API Key:**
   If you wish to use your own API key rather than the existing one, update the `apiKey` variable directly inside the `AtsChecker.cshtml.cs` and `ResumeBuilder.cshtml.cs` Handlers.

3. **Build and Run:**
   ```bash
   dotnet run --project ResumeForgeAI
   ```
4. **View in Browser:**
   Open your browser and navigate to `https://localhost:xxxx` (port will be displayed in the terminal).

## 🧑‍💻 Note for Recruiters & Hiring Managers

**ResumeForgeAI** was built to demonstrate proficiency across the full web development stack:
- **Backend Architecture:** Implementing structured Razor Pages, lightweight JSON data management models (reading/writing historic resumes dynamically to dashboards), and clean separation of concerns in modern ASP.NET Core.
- **Third-Party Integrations:** Interacting with complex external APIs (Groq / Llama 3.1) to extract structured JSON responses and generative chat completions, showcasing prompt engineering and complex asynchronous error handling.
- **Frontend Matrix Tracking:** Advanced usage of Vanilla JS to perform recursive deep-cloning of unique UUID elements that map left-panel DOM input events directly to uniquely constructed DOM preview panes without resorting to frontend React/Vue state frameworks. 
- **Print Optimization:** Fine-tuned targeted CSS (`@page`) controlling how raw DOM nodes synthesize directly into localized PDF download engines without layout breaking.

Feel free to browse the source code. The generative AI logic resides primarily in `Pages/AtsChecker.cshtml.cs` and `Pages/ResumeBuilder.cshtml.cs`, and the responsive UI state structures are best viewed in `Pages/ResumeBuilder.cshtml` & `Pages/Dashboard.cshtml`.

---

## 📬 Contact Information

**Mohammad Naeem**  
- **Email:** [mohammadnaeem.cse@gmail.com](mailto:mohammadnaeem.cse@gmail.com)   
- **LinkedIn:** [linkedin.com/in/mohammad-naeem](https://linkedin.com/in/mohammad-naeem)  
- **GitHub:** [github.com/mohammadnaeem44](https://github.com/mohammadnaeem44)  

---

<p align="center">
  <i>Developed with ❤️ by <a href="https://github.com/mohammadnaeem44">Mohammad Naeem</a></i>
</p>
