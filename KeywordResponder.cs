using System;
using System.Collections.Generic;

namespace ChatBotGui
{
    public class KeywordResponder
    {
        // This dictionary stores security topics as keys and lists of tips as values
        private Dictionary<string, List<string>> responses;

        // A random object used to pick a random index from our lists of tips
        private Random random = new Random();

        // Lists of sentences or phrases that help the bot figure out what the user wants to do
        private List<string> addTaskPhrases = new List<string> { "add task", "add a task", "create task", "i need to", "set up", "enable" };
        private List<string> showLogPhrases = new List<string> { "show activity log", "what have you done", "what did you do", "show log", "recent actions" };
        private List<string> startQuizPhrases = new List<string> { "start quiz", "take quiz", "test my knowledge", "quiz me", "play the game" };

        // Constructor method that sets up the dictionary when the object is created
        public KeywordResponder()
        {
            responses = new Dictionary<string, List<string>>();

            // Call the function below to fill the dictionary with words and tips
            PopulateKeywords();
        }

        // This function fills our dictionary with safety tips for specific keywords
        private void PopulateKeywords()
        {
            // list of security tips for the keyword password
            responses.Add("password", new List<string> {
                "Make sure to use unique, strong passwords with characters, numbers, and symbols.",
                "Never reuse the same password across multiple online accounts or personal profiles."
            });

            // list of security tips for the keyword phishing
            responses.Add("phishing", new List<string> {
                "Always look closely at the sender's email address before clicking any embedded hyperlinks.",
                "Legitimate banks or systems will never ask you to update sensitive info via an unverified link."
            });

            // list of security tips for the keyword privacy
            responses.Add("privacy", new List<string> {
                "Review your social media account configurations to limit who can see your personal uploads.",
                "Be cautious about sharing your physical location, cellphone numbers, or full names publicly."
            });

            // list of security tips for the keyword scam
            responses.Add("scam", new List<string> {
                "If an urgent message demands immediate payment or crypto vouchers, it is likely a trap.",
                "Double-check bizarre offers independently by calling the enterprise directly."
            });
        }

        // This function looks for a keyword in the user's message and returns a random tip if found
        public string GetResponse(string input, out string matchedKey)
        {
            // Convert the user's input text to lowercase to make it easier to match words
            string lowInput = input.ToLower();

            // Loop through all the keyword keys stored inside our responses dictionary
            foreach (var key in responses.Keys)
            {
                // Check if the user's message contains the current keyword key
                if (lowInput.Contains(key))
                {
                    // Save the matching keyword name into our out variable to share it with other classes
                    matchedKey = key;

                    // Generate a random position number based on the total number of tips available for this key
                    int index = random.Next(responses[key].Count);

                    // Return the random tip text from the list
                    return responses[key][index];
                }
            }

            // Set the out variable to empty and return a blank string if no keywords match
            matchedKey = "";
            return "";
        }

        // Checks if the user's message contains any of the phrases used for adding a task
        public bool IsAddTaskIntent(string input)
        {
            // Loop through each phrase inside our add task collection list
            foreach (string phrase in addTaskPhrases)
            {
                // If a matching phrase is found inside the user's input text, return true
                if (input.Contains(phrase))
                {
                    return true;
                }
            }
            // Return false if the loop finishes and no phrases match
            return false;
        }

        // Checks if the user's message contains any of the phrases used for showing the log
        public bool IsShowLogIntent(string input)
        {
            // Loop through each phrase inside our show log collection list
            foreach (string phrase in showLogPhrases)
            {
                // If a matching phrase is found inside the user's input text, return true
                if (input.Contains(phrase))
                {
                    return true;
                }
            }
            // Return false if the loop finishes and no phrases match
            return false;
        }

        // Checks if the user's message contains any of the phrases used for starting the quiz
        public bool IsStartQuizIntent(string input)
        {
            // Loop through each phrase inside our start quiz collection list
            foreach (string phrase in startQuizPhrases)
            {
                // If a matching phrase is found inside the user's input text, return true
                if (input.Contains(phrase))
                {
                    return true;
                }
            }
            // Return false if the loop finishes and no phrases match
            return false;
        }
    }
}