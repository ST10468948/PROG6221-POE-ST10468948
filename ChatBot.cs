using ChatBotGui;
using System;

namespace ChatBotGui
{
    public class ChatBot
    {
        // Global variables to link our other helper class logic modules to this engine
        private KeywordResponder keywords;
        private SentimentDetector sentiment;
        private MemoryStore memory;
        private TaskManager taskEngine;

        // Status variables tracking where we are inside the active conversation flow
        private bool awaitingName = true;
        private string lastTopic = "";

        // Constructor method that sets up the sub-components when the bot is first created
        public ChatBot()
        {
            keywords = new KeywordResponder();
            sentiment = new SentimentDetector();
            memory = new MemoryStore();
            taskEngine = new TaskManager();
        }

        // Returns the initial welcoming greeting text to display when the application boots
        public string GetGreeting()
        {
            return "Bot: Hello! Welcome to the Cybersecurity Awareness Bot. What is your full name?";
        }

        // The central processing engine method that evaluates text strings typed by users
        public string ProcessInput(string input)
        {
            // Stop processing and return a message if the user types absolutely nothing or blank spaces
            if (string.IsNullOrWhiteSpace(input))
            {
                return "Bot: I didn't catch that. Please type a message or ask a question!";
            }

            // Check if this is the very first loop interaction where we explicitly expect their name
            if (awaitingName)
            {
                // Save the typed value into our profile memory tool
                memory.Store("name", input);

                // Flip the status flag to false so we skip this registration loop block next time
                awaitingName = false;

                return $"Bot: Nice to meet you, {memory.UserName}! You can ask me about 'passwords', 'phishing', 'privacy', or 'scams'. What's on your mind today?";
            }

            // Flatten the input text string to lowercase to prevent matching errors caused by casing variations
            string cleanInput = input.ToLower();

            // Check our keywords helper to see if the user's sentence matches a task addition intent phrase
            if (keywords.IsAddTaskIntent(cleanInput))
            {
                string extractedTitle = "New Cyber Task";

                // Strip away the matching command phrase prefix from the sentence to isolate the task text content
                if (cleanInput.Contains("add task")) extractedTitle = input.Replace("add task", "").Replace("Add task", "").Trim();
                else if (cleanInput.Contains("add a task")) extractedTitle = input.Replace("add a task", "").Replace("Add a task", "").Trim();
                else if (cleanInput.Contains("create task")) extractedTitle = input.Replace("create task", "").Replace("Create task", "").Trim();
                else extractedTitle = input.Trim();

                // Trim trailing periods, spaces, or exclamation marks from the isolated task title text string
                extractedTitle = extractedTitle.Trim(new char[] { ' ', '.', ',', '!', ':' });

                // Call our task business logic system layer to write this new row directly to the JSON document
                taskEngine.AddTask(extractedTitle, "Task created via conversational chatbot command input row.", "No reminder set");

                // Note this exact text translation match directly inside our running system activity logs
                ActivityLogger.Log($"NLP recognized task intent from input: '{input}'");

                return $"Bot: Task added: '{extractedTitle}'. Would you like to set a reminder for this task?";
            }

            // Check our keywords helper to see if the user's sentence matches an activity log viewing request
            if (keywords.IsShowLogIntent(cleanInput))
            {
                // Record the log viewing event inside the history ledger list
                ActivityLogger.Log("NLP recognized activity log request trigger statement.");

                // Return the 10 most recent numbered history entry strings directly back to the chat viewer layout
                return ActivityLogger.GetRecentLog(10);
            }

            // Intercept special extra commands matching requests to view hidden historic tracking items
            if (cleanInput.Contains("show more"))
            {
                return ActivityLogger.GetFullLog();
            }

            // Check our keywords helper to see if the user's sentence matches a quiz invocation shortcut statement
            if (keywords.IsStartQuizIntent(cleanInput))
            {
                ActivityLogger.Log("NLP recognized shortcut quiz request trigger statement.");
                return "Bot: Let's test your skills! Click on the 'Cyber Quiz' tab at the top of the screen right now to play our interactive mini-game.";
            }

            // Check if the user is asking a conversational follow-up question regarding the last discussed topic
            if (cleanInput.Contains("tell me more") || cleanInput.Contains("explain more") || cleanInput.Contains("another tip"))
            {
                // Verify that we actually have a valid previous topic stored in our loop tracker history
                if (!string.IsNullOrEmpty(lastTopic))
                {
                    // Fetch an alternative random tip string using the previously matching dictionary key string
                    string followUpTip = keywords.GetResponse(lastTopic, out _);
                    return $"Bot: Sure! Here is more insight regarding {lastTopic}: {followUpTip}";
                }
                return "Bot: What specific cybersecurity topic would you like me to elaborate on first?";
            }

            // Handle casual small talk input variations to keep interactions feeling organic
            if (cleanInput.Contains("how are you"))
            {
                return $"Bot: I am functioning perfectly, thanks for checking in, {memory.UserName}! Ready to clean up cyber threats.";
            }
            if (cleanInput.Contains("purpose"))
            {
                return "Bot: My objective is to educate South African citizens on identifying and avoiding common cyber risks.";
            }

            // Pass the input string parameter to our mood tracking detector class to evaluate user emotions
            Sentiment detectedMood = sentiment.Detect(input);
            string moodOpener = sentiment.GetSentimentResponse(detectedMood);

            // Pass the input sentence string to our security keyword checker to extract matching response strings
            string matchedKey;
            string coreTip = keywords.GetResponse(input, out matchedKey);

            // Verify that a valid text string payload was found and retrieved from the dictionary asset collection
            if (!string.IsNullOrEmpty(coreTip))
            {
                // If they phrase an explicit long-term affinity, pass the metric value to store it inside memory
                if (cleanInput.Contains("interested in") || cleanInput.Contains("favorite topic is"))
                {
                    memory.Store("topic", matchedKey);
                }

                // Retrieve custom profile response prefix parameters and update our last topic pointer tracker variable
                string personalizedPrefix = memory.GetPersonalisedOpener();
                lastTopic = matchedKey;

                // Document the matching dictionary tip event directly inside our history list records
                ActivityLogger.Log($"Keyword matched: {matchedKey} - response delivered");

                // Combine the localized emotional opener, custom profile prefix, and the random educational safety tip
                return $"Bot: {moodOpener}{personalizedPrefix}{coreTip}";
            }

            // Fallback response if the clean string value fails to trigger any matching rules or text dictionary tags
            return "Bot: I didn't quite catch the specifics of that query. Could you please rephrase or try mentioning passwords, scams, or phishing?";
        }
    }
}