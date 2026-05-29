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
            // Change user input to lowercase so searching is easier
            string lowInput = input.ToLower();

            // Check every keyword we have in our dictionary
            foreach (var key in responses.Keys)
            {
                // If the user's message contains our keyword...
                if (lowInput.Contains(key))
                {
                    matchedKey = key; // Tell the program which keyword we found

                    // Pick a random number based on how many tips we have for this topic
                    int index = random.Next(responses[key].Count);

                    // Return that random tip from the list
                    return responses[key][index];
                }
            }

            // If no keywords were found, return an empty result
            matchedKey = "";
            return "";
        }
    }
}