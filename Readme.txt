# Cybersecurity Awareness Assistant (v2.0)

A lightweight, interactive desktop application designed to educate citizens on identifying and avoiding common cyber risks. The application utilizes a retro-themed graphical interface to process user text, detect user sentiment, and offer tailored security advice.

# Key Features

* Adaptive Conversations:** Remembers user names and favorite topics for a personalized chat experience.
* Sentiment Detection:** Analyzes the mood of user inputs (e.g., Worried, Curious, Frustrated) and alters its tone dynamically.
* Keyword Matching System:** Provides helpful tips instantly when triggers like `password`, `phishing`, `privacy`, or `scam` are detected.
* Visual Assets:** Renders retro terminal aesthetics including a dedicated ASCII art display canvas.

---

# Project Structure & Architecture

The application is built using a clean, modular structure split between UI presentation and core logic:

| Component / Class | Type | Purpose |
| --- | --- | --- |
| **`MainWindow.xaml`** | UI | The layout engine handling the retro theme (Cyan/Aqua borders, DarkSlateBlue backgrounds), chat scroller, and input boxes. |
| **`ChatBot.cs`** | Logic | The central "brain" orchestrating input flow, state tracking, and responses. |
| **`SentimentDetector.cs`** | Logic | Evaluates user vocabulary to determine emotional states. |
| **`KeywordResponder.cs`** | Logic | Houses the security knowledge base and delivers randomized advisory tips. |
| **`MemoryStore.cs`** | Logic | Acts as the data cache for the active session (storing user profile details). |

---

# Tech Stack & Requirements

* **Framework:** .NET / WPF (Windows Presentation Foundation)
* **Language:** C#
* **IDE:** Visual Studio 2022 (or newer)

## 🏃 Getting Started

References/Sources

*GITHUB, 2026. GitHub Copilot, computer software. Available at: https://github.com/features/copilot [Accessed 25 May 2026].
Fâciu, A. (2012) Multiline Textbox with automatic vertical scroll. [Online]. Available at: https://stackoverflow.com/questions/8938775/ (Accessed 29 May 2026).

*McInroy, L. (2014) How to play a sound file?. [Online]. Available at: https://stackoverflow.com/questions/22745278/ (Accessed 25 May 2026).

*Rawling (2012) C# - Dictionary of Lists. [Online]. Available at: https://stackoverflow.com/questions/13601151/ (Accessed 25 May 2026).

*Rupp, J. (2009) Random entry from dictionary. [Online]. Available at: https://stackoverflow.com/questions/1028136/ (Accessed 25 May 2026).