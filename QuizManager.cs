using System;
using System.Collections.Generic;

namespace ChatBotGui
{
    // This class manages the quiz gameplay, tracking the questions and score
    public class QuizManager
    {
        // A list container to store our 10 cybersecurity questions
        private List<QuizQuestion> _questions;

        // Tracks which question number the user is currently on
        private int _currentIndex;

        // Tracks how many questions the user answered correctly
        private int _score;

        // Constructor: initializes the list and loads the 10 questions automatically
        public QuizManager()
        {
            _questions = new List<QuizQuestion>();
            _currentIndex = 0;
            _score = 0;

            LoadQuizQuestions();
        }

        // Populates our list with 10 distinct cybersecurity questions
        private void LoadQuizQuestions()
        {
            _questions.Add(new QuizQuestion
            {
                QuestionText = "1. Which of the following is considered a strong password practice?",
                Options = new List<string> { "A) Using your birthdate", "B) Reusing the same password for all accounts", "C) Combining uppercase, lowercase, numbers, and symbols", "D) Writing it on a sticky note on your monitor" },
                CorrectAnswer = "C",
                Explanation = "Strong passwords use a mix of different character types to increase complexity and resist guessing attacks."
            });

            _questions.Add(new QuizQuestion
            {
                QuestionText = "2. You receive an email from your bank asking you to click a link and verify your login details immediately. What should you do?",
                Options = new List<string> { "A) Click the link and log in quickly", "B) Delete or report it; banks do not ask for passwords via email", "C) Reply to the email asking if it is real", "D) Forward it to your friends" },
                CorrectAnswer = "B",
                Explanation = "Legitimate institutions will never ask you to reveal private credentials or click direct validation links via email."
            });

            _questions.Add(new QuizQuestion
            {
                QuestionText = "3. True or False: If a website has 'https://' and a padlock icon, it means the website is 100% safe and safe from scammers.",
                Options = new List<string> { "A) True", "B) False" },
                CorrectAnswer = "B",
                Explanation = "False. HTTPS only means the connection is encrypted. Scammers can easily set up encrypted phishing websites too."
            });

            _questions.Add(new QuizQuestion
            {
                QuestionText = "4. What is the main goal of Social Engineering attacks?",
                Options = new List<string> { "A) Guessing your WiFi password", "B) Tricking humans into giving away private information", "C) Stealing files from a backup hard drive", "D) Crashing your web browser" },
                CorrectAnswer = "B",
                Explanation = "Social engineering targets human psychology, manipulating trust, fear, or urgency to exploit user vulnerabilities."
            });

            _questions.Add(new QuizQuestion
            {
                QuestionText = "5. Why should you avoid using public Wi-Fi networks (like at a coffee shop) for online banking?",
                Options = new List<string> { "A) The internet speed is too slow", "B) Public networks can be intercepted by hackers to steal data", "C) Banking websites block public Wi-Fi connections", "D) It drains your mobile battery faster" },
                CorrectAnswer = "B",
                Explanation = "Unsecured public networks allow attackers on the same network to intercept unencrypted data packets easily."
            });

            _questions.Add(new QuizQuestion
            {
                QuestionText = "6. What is Multi-Factor Authentication (MFA / 2FA)?",
                Options = new List<string> { "A) Changing your password every 30 days", "B) Requiring two or more verification methods to access an account", "C) Setting up multiple passwords for one account", "D) Installing anti-virus software on multiple devices" },
                CorrectAnswer = "B",
                Explanation = "MFA adds a critical extra shield layer. Even if a hacker steals your password, they still cannot gain entry without your physical token or phone code."
            });

            _questions.Add(new QuizQuestion
            {
                QuestionText = "7. You download a file, and your anti-virus software triggers a malware warning alert. What is the safest action?",
                Options = new List<string> { "A) Ignore it and run the file anyway", "B) Disable your anti-virus for 5 minutes", "C) Delete the file completely immediately", "D) Rename the file and try opening it again" },
                CorrectAnswer = "C",
                Explanation = "Always trust your system security alerts. Wiping malicious code fragments immediately prevents system background exploitation."
            });

            _questions.Add(new QuizQuestion
            {
                QuestionText = "8. True or False: Backing up your files regularly protects you from losing data during a Ransomware attack.",
                Options = new List<string> { "A) True", "B) False" },
                CorrectAnswer = "A",
                Explanation = "True. If ransomware encrypts your device and demands a payment fee, you can simply format it and restore your records safely from your clean external backup."
            });

            _questions.Add(new QuizQuestion
            {
                QuestionText = "9. Which of the following is a sign that a website might be a phishing scam?",
                Options = new List<string> { "A) High-resolution images", "B) Slight typos or mismatched characters in the URL address bar", "C) Clear privacy terms and conditions links", "D) Simple dark mode color schemes" },
                CorrectAnswer = "B",
                Explanation = "Phishing websites often spoof official domain markers using lookalike domains (like 'micros0ft.com') to trick victims."
            });

            _questions.Add(new QuizQuestion
            {
                QuestionText = "10. What should you do before selling or throwing away an old smartphone?",
                Options = new List<string> { "A) Simply turn it off", "B) Delete your photos folder only", "C) Perform a full factory reset and data wipe", "D) Take out the battery container box only" },
                CorrectAnswer = "C",
                Explanation = "A full factory data reset completely clears local identity artifacts, keeping cached user credentials away from third parties."
            });
        }

        // Returns the question object that the user is currently answering
        public QuizQuestion GetCurrentQuestion()
        {
            return _questions[_currentIndex];
        }

        // Checks if the user's chosen answer matches the correct one, adjusts score, and increments index
        public bool SubmitAnswer(string selectedLetter)
        {
            bool isCorrect = (selectedLetter == _questions[_currentIndex].CorrectAnswer);

            if (isCorrect)
            {
                _score++; // Increment score count if answer is correct
            }

            _currentIndex++; // Advance the game to the next question index position
            return isCorrect;
        }

        // Checks if the user has answered all 10 questions
        public bool IsFinished()
        {
            return _currentIndex >= _questions.Count;
        }

        // Pulls the current score integer state variable
        public int GetScore()
        {
            return _score;
        }

        // Pulls the total question allocation count
        public int GetTotalQuestions()
        {
            return _questions.Count;
        }

        // Generates a concluding feedback statement based on the user's performance score
        public string GetFinalMessage()
        {
            if (_score >= 8)
            {
                return "Great job! You're a cybersecurity pro!"; // Top band feedback [cite: 345]
            }
            else
            {
                return "Keep learning to stay safe online!"; // Practice feedback [cite: 346]
            }
        }

        // Resets the entire quiz state variables so the user can try again
        public void ResetQuiz()
        {
            _currentIndex = 0;
            _score = 0;
        }
    }
}