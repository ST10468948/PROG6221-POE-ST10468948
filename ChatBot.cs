using ChatBotGui;
using System;

namespace ChatBotGui
{
    public class ChatBot
    {
        // These link the other parts of our program (keywords, mood detector, and memory)
        private KeywordResponder keywords;
        private SentimentDetector sentiment;
        private MemoryStore memory;

        // These keep track of the conversation state (like "did we ask for their name yet?")
        private bool awaitingName = true;
        private string lastTopic = "";

        public ChatBot()
        {
            // Set up all the tools the bot needs when it is first created
            keywords = new KeywordResponder();
            sentiment = new SentimentDetector();
            memory = new MemoryStore();
        }

        // The very first message the bot says when the app starts
        public string GetGreeting()
        {
            return "Bot: Hello! Welcome to the Cybersecurity Awareness Bot. What is your full name?";
        }

        // This is the main engine that handles everything the user types
        public string ProcessInput(string input)
        {
            // Check if the user sent an empty message
            if (string.IsNullOrWhiteSpace(input))
            {
                return "Bot: I didn't catch that. Please type a message or ask a question!";
            }

            // STEP 1: If we don't have the user's name yet, save it first
            if (awaitingName)
            {
                memory.Store("name", input); // Save the name in memory
                awaitingName = false;        // We don't need to ask for the name anymore
                return $"Bot: Nice to meet you, {memory.UserName}! You can ask me about 'passwords', 'phishing', 'privacy', or 'scams'. What's on your mind today?";
            }

            string cleanInput = input.ToLower();

            // STEP 2: Check if the user is asking for more info on the previous topic
            if (cleanInput.Contains("tell me more") || cleanInput.Contains("explain more") || cleanInput.Contains("another tip"))
            {
                if (!string.IsNullOrEmpty(lastTopic))
                {
                    // Get another random tip using the topic we talked about last
                    string followUpTip = keywords.GetResponse(lastTopic, out _);
                    return $"Bot: Sure! Here is more insight regarding {lastTopic}: {followUpTip}";
                }
                return "Bot: What specific cybersecurity topic would you like me to elaborate on first?";
            }

            // STEP 3: Simple "Small Talk" check
            if (cleanInput.Contains("how are you"))
            {
                return $"Bot: I am functioning perfectly, thanks for checking in, {memory.UserName}! Ready to clean up cyber threats.";
            }
            if (cleanInput.Contains("purpose"))
            {
                return "Bot: My objective is to educate South African citizens on identifying and avoiding common cyber risks.";
            }

            // STEP 4: Analyze the user's mood (Sentiment)
            Sentiment detectedMood = sentiment.Detect(input);
            string moodOpener = sentiment.GetSentimentResponse(detectedMood);

            // STEP 5: Search for security keywords (passwords, scams, etc.)
            string matchedKey;
            string coreTip = keywords.GetResponse(input, out matchedKey);

            if (!string.IsNullOrEmpty(coreTip))
            {
                // If they mention a favorite topic, save it to their profile (Memory)
                if (cleanInput.Contains("interested in") || cleanInput.Contains("favorite topic is"))
                {
                    memory.Store("topic", matchedKey);
                }

                string personalizedPrefix = memory.GetPersonalisedOpener();
                lastTopic = matchedKey; // Remember this topic for follow-up questions

                // Combine the mood response, personal greeting, and the security tip
                return $"Bot: {moodOpener}{personalizedPrefix}{coreTip}";
            }

            // STEP 6: If the bot doesn't know what the user is talking about
            return "Bot: I didn't quite catch the specifics of that query. Could you please rephrase or try mentioning passwords, scams, or phishing?";
        }
    }
}