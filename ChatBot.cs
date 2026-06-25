using ChatBotGui;
using System;

namespace ChatBotGui
{
    public class ChatBot
    {
        // These link the other parts of our program
        private KeywordResponder keywords;
        private SentimentDetector sentiment;
        private MemoryStore memory;

        // Connect our chatbot engine directly to our task manager logic class
        private TaskManager taskEngine;

        // These keep track of the conversation state
        private bool awaitingName = true;
        private string lastTopic = "";

        public ChatBot()
        {
            // Set up all the tools the bot needs when it is first created
            keywords = new KeywordResponder();
            sentiment = new SentimentDetector();
            memory = new MemoryStore();
            taskEngine = new TaskManager(); // Initialize our task engine layer
        }

        public string GetGreeting()
        {
            return "Bot: Hello! Welcome to the Cybersecurity Awareness Bot. What is your full name?";
        }

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
                memory.Store("name", input);
                awaitingName = false;
                return $"Bot: Nice to meet you, {memory.UserName}! You can ask me about 'passwords', 'phishing', 'privacy', or 'scams'. What's on your mind today?";
            }

            string cleanInput = input.ToLower();

            // =========================================================================
            // NEW TASK 3: SIMULATED NLP INTENT DETECTION INTERCEPTORS
            // =========================================================================

            // 1. NLP Check: Detect if user wants to add a task from varied phrasing
            if (keywords.IsAddTaskIntent(cleanInput))
            {
                string extractedTitle = "New Cyber Task";

                // Extract the task name context by cutting out the trigger phrases
                if (cleanInput.Contains("add task")) extractedTitle = input.Replace("add task", "").Replace("Add task", "").Trim();
                else if (cleanInput.Contains("add a task")) extractedTitle = input.Replace("add a task", "").Replace("Add a task", "").Trim();
                else if (cleanInput.Contains("create task")) extractedTitle = input.Replace("create task", "").Replace("Create task", "").Trim();
                else extractedTitle = input.Trim();

                // Clean up any stray symbols or periods at the end of their sentence
                extractedTitle = extractedTitle.Trim(new char[] { ' ', '.', ',', '!', ':' });

                // Add the task automatically through our storage engine system
                taskEngine.AddTask(extractedTitle, "Task created via conversational chatbot command input row.", "No reminder set");

                // Log this NLP action to our running Activity Logger class
                ActivityLogger.Log($"NLP recognized task intent from input: '{input}'");

                return $"Bot: Task added: '{extractedTitle}'. Would you like to set a reminder for this task?";
            }

            // 2. NLP Check: Detect if user wants to view the activity log
            if (keywords.IsShowLogIntent(cleanInput))
            {
                ActivityLogger.Log("NLP recognized activity log request trigger statement.");
                // Return the formatted text block containing recent entries directly to the chat
                return ActivityLogger.GetRecentLog(10);
            }

            // Extra NLP Check: Catch 'show more' requests inside the chat window
            if (cleanInput.Contains("show more"))
            {
                return ActivityLogger.GetFullLog();
            }

            // 3. NLP Check: Detect if user wants to open the quiz game via conversation shortcuts
            if (keywords.IsStartQuizIntent(cleanInput))
            {
                ActivityLogger.Log("NLP recognized shortcut quiz request trigger statement.");
                return "Bot: Let's test your skills! Click on the 'Cyber Quiz' tab at the top of the screen right now to play our interactive mini-game.";
            }

            // =========================================================================
            // ORIGINAL FEATURES (Conversation Flow, Sentiment, & Keyword Processing)
            // =========================================================================

            // STEP 2: Check if the user is asking for more info on the previous topic
            if (cleanInput.Contains("tell me more") || cleanInput.Contains("explain more") || cleanInput.Contains("another tip"))
            {
                if (!string.IsNullOrEmpty(lastTopic))
                {
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
                if (cleanInput.Contains("interested in") || cleanInput.Contains("favorite topic is"))
                {
                    memory.Store("topic", matchedKey);
                }

                string personalizedPrefix = memory.GetPersonalisedOpener();
                lastTopic = matchedKey;

                // Log that a standard keyword tip match was delivered successfully
                ActivityLogger.Log($"Keyword matched: {matchedKey} - response delivered");

                return $"Bot: {moodOpener}{personalizedPrefix}{coreTip}";
            }

            // STEP 6: Default fallback if the bot completely misses intent phrasings
            return "Bot: I didn't quite catch the specifics of that query. Could you please rephrase or try mentioning passwords, scams, or phishing?";
        }
    }
}