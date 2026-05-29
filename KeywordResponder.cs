using System;
using System.Collections.Generic;

namespace ChatBotGui
{
    public class KeywordResponder
    {
        
        private Dictionary<string, List<string>> responses;
        private Random random = new Random();

        public KeywordResponder()
        {
            responses = new Dictionary<string, List<string>>();
            PopulateKeywords();
        }

        private void PopulateKeywords()
        {
            
            responses.Add("password", new List<string> {
                "Make sure to use unique, strong passwords with characters, numbers, and symbols.",
                "Never reuse the same password across multiple online accounts or personal profiles."
            });

            
            responses.Add("phishing", new List<string> {
                "Always look closely at the sender's email address before clicking any embedded hyperlinks.",
                "Legitimate banks or systems will never ask you to update sensitive info via an unverified link."
            });

            
            responses.Add("privacy", new List<string> {
                "Review your social media account configurations to limit who can see your personal uploads.",
                "Be cautious about sharing your physical location, cellphone numbers, or full names publicly."
            });

            
            responses.Add("scam", new List<string> {
                "If an urgent message demands immediate payment or crypto vouchers, it is likely a trap.",
                "Double-check bizarre offers independently by calling the enterprise directly."
            });
        }

        
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
    }
}