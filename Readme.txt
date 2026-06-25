# Cybersecurity Awareness Assistant (v3.2)

A lightweight, interactive desktop application designed to educate South African citizens on identifying and avoiding common cyber risks. The application uses a retro terminal style interface to chat with users, detect how they are feeling, and provide personalized security advice. 

This project expands on our previous chatbot by adding a Task Assistant, an educational Quiz game, a natural language intent simulation, and an automatically tracked Activity Log.

---

## 👤 Student Information
* **Student Name:** Sedzani E. Sikhwari
* **Student Number:** ST10468948

---

## 🚀 Key Features

Our expanded application includes features across all project phases:

### 💬 Chatbot & Core Features (From Parts 1 & 2)
* **Personalized Memory:** Remembers your name and tailors conversation logs to your profile.
* **Sentiment Mood Tracking:** Analyzes user sentences to see if they are worried, curious, or frustrated and modifies responses to match.
* **Knowledge Database Triggers:** Instantly scans text and provides safety hints for keywords like password, phishing, privacy, and scam.
* **Retro Visuals:** Shows unique terminal themes with an integrated ASCII art display banner canvas.

### 🛠️ New Advanced Modules (Part 3 / POE additions)
* **Task Assistant Panel:** A complete task tracker tab to create, view, mark as finished, and delete security checklists.
* **JSON Database Storage:** All user checklists automatically sync with a local tasks.json file so no text information is lost when the program closes.
* **Cybersecurity Quiz Mini-Game:** An interactive 10+ question multiple-choice and true/false game that displays one question at a time and gives instant educational explanations.
* **Simulated NLP Intent Detection:** The chat window can automatically catch commands like "Add a task to enable 2FA" or "Show log" even if phrased in different ways.
* **System Activity Log:** Automatically documents backend actions with modern timestamps and allows users to browse histories directly inside the interface.

---

## 💻 Tech Stack & Requirements

* **Framework:** .NET 8.0 / WPF (Windows Presentation Foundation)
* **Programming Language:** C#
* **Required IDE:** Visual Studio 2022 (or newer)
* **External NuGet Packages:** Newtonsoft.Json (v13.0.3 or latest stable)

---

## 🛠️ Step-by-Step Setup Instructions

Follow these simple steps to set up and run the source code on your local computer:

### Step 1: Clone or Download the Project
Download the complete project folder from the public GitHub link or extract the backup ZIP archive file to a location on your computer. Make sure the folder structure does not include the temporary bin/ or obj/ compilation directories.

### Step 2: Open the Solution File
Launch Visual Studio 2022. Go to File -> Open -> Project/Solution, select the CybersecurityChatbot.sln file from the root directory, and wait for the workspace project files to load.

### Step 3: Install the Newtonsoft.Json Package
Because standard C# does not handle JSON operations easily by default, you must install the required Newtonsoft.Json package:
1. Right-click on your project name (CybersecurityChatbot) in the right-hand Solution Explorer window.
2. Click on Manage NuGet Packages... from the options drop-down menu.
3. Click on the Browse tab, type Newtonsoft.Json into the search bar, select it, and click the Install button.

### Step 4: Position Your Media Sound Files
Make sure your voice introduction greeting sound file is named exactly sage.wav (or greeting.wav) and is placed directly inside your main project output folder where your executable code builds. In Visual Studio, you can right-click the audio asset, choose Properties, and set Copy to Output Directory to Copy if newer.

### Step 5: Run the Code
Press the green Start / Debug button at the top of Visual Studio to compile and launch the application interface window. 

Note: You do not need to set up an external server or database software. The file tasks.json is created completely automatically by our code tracking system the exact moment you save your very first task entry.

---

## 📁 Repository Structure & Architecture

Our program uses a modular split layout to keep visual code and back-end rules separate:

* MainWindow.xaml — The visual frontend UI file holding the retro theme design, tab items, chat scroller, text input boxes, and grid view tables.
* MainWindow.xaml.cs — The code-behind file containing event handlers for user button clicks and keyboard commands.
* ChatBot.cs — The central engine brain that manages intent routing, conversation flows, and mood adjustments.
* KeywordResponder.cs — Stores the collection of educational cybersecurity tip definitions and matches phrase intent arrays.
* SentimentDetector.cs — Evaluates user typing styles to calculate emotional variations.
* MemoryStore.cs — Caches user details like name profile data throughout the session.
* CyberTask.cs — The model data structure class outlining properties for a single task row.
* TaskManager.cs — Coordinates business operations for passing task datasets between the user interface and file handlers.
* TaskStorageHelper.cs — Dedicated file reading and writing engine that serializes data lists directly to disk.
* QuizManager.cs — Controls game loops, tracking lists, multiple-choice options, scoring systems, and feedback texts.
* ActivityLogger.cs — Records system operational updates and appends clean tracking labels with system time values.

---

## 📜 Development Commit History Milestones

The source code repository follows a sequential branch commit message structure to log progress milestones:

1. feat: Add JSON task storage with TaskStorageHelper and CyberTask model
2. feat: Add task assistant panel with add, view, complete, and delete via JSON file
3. feat: Add cybersecurity quiz mini-game with 10+ questions and immediate feedback
4. feat: Add activity log with timestamps, 10 entry limit, and show more option
5. feat: Expand NLP to detect task, quiz, and log intents from varied phrasings
6. docs: Final integration test passed, README updated with Newtonsoft.Json NuGet setup instructions

---

## 🏷️ Tagged Version Releases

This project includes a minimum of 3 official tagged version releases published on GitHub with detailed notes:
* v3.0 — Initial deployment phase covering the Task Assistant layout workspace alongside functional local JSON arrays.
* v3.1 — Intermediate release adding the multiple-choice quiz system mechanics and internal time-stamped log listings.
* v3.2 — Final release build combining all three parts into a cohesive, professional user experience.

---

## 📝 References & Source Attributions

* Fâciu, A. (2012) Multiline Textbox with automatic vertical scroll. [Online]. Available at: https://stackoverflow.com/questions/8938775/ (Accessed 29 May 2026).
* GITHUB. (2026) GitHub Copilot, computer software. Available at: https://github.com/features/copilot (Accessed 25 May 2026).
* McInroy, L. (2014) How to play a sound file?. [Online]. Available at: https://stackoverflow.com/questions/22745278/ (Accessed 25 May 2026).
* Newtonsoft (2026) Json.NET Documentation. [Online]. Available at: https://www.newtonsoft.com/json (Accessed 25 May 2026).
* Rawling (2012) C# - Dictionary of Lists. [Online]. Available at: https://stackoverflow.com/questions/13601151/ (Accessed 25 May 2026).
* Rupp, J. (2009) Random entry from dictionary. [Online]. Available at: https://stackoverflow.com/questions/1028136/ (Accessed 25 May 2026).