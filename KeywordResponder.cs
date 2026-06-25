using System;
using System.Collections.Generic;

namespace ChatBotGui
{
    public class KeywordResponder
    {
        // This dictionary links a "topic" (like password) to a list of helpful tips
        private Dictionary<string, List<string>> responses;

        // Used to pick a random tip so the bot doesn't repeat the same thing every time
        private Random random = new Random();

        // New phrase lists to help detect different ways a user might phrase an action (NLP Simulation)
        private List<string> addTaskPhrases = new List<string> { "add task", "add a task", "create task", "i need to", "set up", "enable" };
        private List<string> showLogPhrases = new List<string> { "show activity log", "what have you done", "what did you do", "show log", "recent actions" };
        private List<string> startQuizPhrases = new List<string> { "start quiz", "take quiz", "test my knowledge", "quiz me", "play the game" };

        public KeywordResponder()
        {
            responses = new Dictionary<string, List<string>>();
            // Run the setup function to fill our dictionary with data
            PopulateKeywords();
        }

        private void PopulateKeywords()
        {
            // Add security tips for "password"
            responses.Add("password", new List<string> {
                "Make sure to use unique, strong passwords with characters, numbers, and symbols.",
                "Never reuse the same password across multiple online accounts or personal profiles."
            });

            // Add security tips for "phishing"
            responses.Add("phishing", new List<string> {
                "Always look closely at the sender's email address before clicking any embedded hyperlinks.",
                "Legitimate banks or systems will never ask you to update sensitive info via an unverified link."
            });

            // Add security tips for "privacy"
            responses.Add("privacy", new List<string> {
                "Review your social media account configurations to limit who can see your personal uploads.",
                "Be cautious about sharing your physical location, cellphone numbers, or full names publicly."
            });

            // Add security tips for "scam"
            responses.Add("scam", new List<string> {
                "If an urgent message demands immediate payment or crypto vouchers, it is likely a trap.",
                "Double-check bizarre offers independently by calling the enterprise directly."
            });
        }

        // This looks for a keyword in the user's message and picks a random tip
        public string GetResponse(string input, out string matchedKey)
        {
            string lowInput = input.ToLower();

            foreach (var key in responses.Keys)
            {
                if (lowInput.Contains(key))
                {
                    matchedKey = key;
                    int index = random.Next(responses[key].Count);
                    return responses[key][index];
                }
            }

            matchedKey = "";
            return "";
        }

        // NLP Check: Returns true if the input contains any phrase related to adding a task
        public bool IsAddTaskIntent(string input)
        {
            foreach (string phrase in addTaskPhrases)
            {
                if (input.Contains(phrase))
                {
                    return true;
                }
            }
            return false;
        }

        // NLP Check: Returns true if the input contains any phrase related to viewing the activity log
        public bool IsShowLogIntent(string input)
        {
            foreach (string phrase in showLogPhrases)
            {
                if (input.Contains(phrase))
                {
                    return true;
                }
            }
            return false;
        }

        // NLP Check: Returns true if the input contains any phrase related to starting the quiz
        public bool IsStartQuizIntent(string input)
        {
            foreach (string phrase in startQuizPhrases)
            {
                if (input.Contains(phrase))
                {
                    return true;
                }
            }
            return false;
        }
    }
}