using ChatBotGui;
using System;

namespace ChatBotGui
{
    public class ChatBot
    {
        private KeywordResponder keywords;
        private SentimentDetector sentiment;
        private MemoryStore memory;

        private bool awaitingName = true; 
        private string lastTopic = "";    

        public ChatBot()
        {
            keywords = new KeywordResponder();
            sentiment = new SentimentDetector();
            memory = new MemoryStore();
        }

        public string GetGreeting()
        {
            return "Bot: Hello! Welcome to the Cybersecurity Awareness Bot. What is your full name?";
        }

        public string ProcessInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return "Bot: I didn't catch that. Please type a message or ask a question!"; 
            }

            
            if (awaitingName)
            {
                memory.Store("name", input);
                awaitingName = false;
                return $"Bot: Nice to meet you, {memory.UserName}! You can ask me about 'passwords', 'phishing', 'privacy', or 'scams'. What's on your mind today?";
            }

            string cleanInput = input.ToLower();

            
            if (cleanInput.Contains("tell me more") || cleanInput.Contains("explain more") || cleanInput.Contains("another tip"))
            {
                if (!string.IsNullOrEmpty(lastTopic))
                {
                    string followUpTip = keywords.GetResponse(lastTopic, out _);
                    return $"Bot: Sure! Here is more insight regarding {lastTopic}: {followUpTip}";
                }
                return "Bot: What specific cybersecurity topic would you like me to elaborate on first?";
            }

            
            if (cleanInput.Contains("how are you"))
            {
                return $"Bot: I am functioning perfectly, thanks for checking in, {memory.UserName}! Ready to clean up cyber threats.";
            }
            if (cleanInput.Contains("purpose"))
            {
                return "Bot: My objective is to educate South African citizens on identifying and avoiding common cyber risks.";
            }

            
            Sentiment detectedMood = sentiment.Detect(input);
            string moodOpener = sentiment.GetSentimentResponse(detectedMood);

            
            string matchedKey;
            string coreTip = keywords.GetResponse(input, out matchedKey);

            if (!string.IsNullOrEmpty(coreTip))
            {
                
                if (cleanInput.Contains("interested in") || cleanInput.Contains("favorite topic is"))
                {
                    memory.Store("topic", matchedKey);
                }

                string personalizedPrefix = memory.GetPersonalisedOpener();
                lastTopic = matchedKey; 

                
                return $"Bot: {moodOpener}{personalizedPrefix}{coreTip}";
            }

            
            return "Bot: I didn't quite catch the specifics of that query. Could you please rephrase or try mentioning passwords, scams, or phishing?";
        }
    }
}