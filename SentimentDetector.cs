using System;
using System.Collections.Generic;

namespace ChatBotGui
{// This is a list of all the different moods our program can recognize
    public enum Sentiment { Neutral, Worried, Curious, Frustrated, Happy }

    public class SentimentDetector
    {
        // This stores our 'library' of words and which mood they belong to
        private Dictionary<Sentiment, List<string>> triggerWords;

        public SentimentDetector()
        {
            // Set up the library (Dictionary)
            triggerWords = new Dictionary<Sentiment, List<string>>();

            // Fill the library with specific words for each mood
            triggerWords.Add(Sentiment.Worried, new List<string> { "worried", "scared", "afraid", "anxious", "unsafe" });
            triggerWords.Add(Sentiment.Curious, new List<string> { "curious", "wondering", "interested", "how does" });
            triggerWords.Add(Sentiment.Frustrated, new List<string> { "frustrated", "annoyed", "confused", "don't understand" });
        }

        // This function looks at what the user typed and tries to guess their mood
        public Sentiment Detect(string input)
        {
            // Make everything lowercase so we don't get confused by Capital letters
            string lowInput = input.ToLower();

            // Look through every mood in our library
            foreach (var pair in triggerWords)
            {
                // Look through every word inside that mood's list
                foreach (string word in pair.Value)
                {
                    // If the user's sentence has the word in it, return that mood!
                    if (lowInput.Contains(word))
                    {
                        return pair.Key;
                    }
                }
            }
            // If we didn't find any matches, just stay neutral
            return Sentiment.Neutral;
        }

        // This gives back a nice message based on the mood we detected
        public string GetSentimentResponse(Sentiment s)
        {
            // "Switch" is like a shortcut for a long list of "if" statements
            switch (s)
            {
                case Sentiment.Worried:
                    return "It is completely understandable to feel anxious about online security. Let's tackle it safely. ";
                case Sentiment.Curious:
                    return "I am glad you are exploring this topic! Knowledge is your best shield. ";
                case Sentiment.Frustrated:
                    return "Tech can be incredibly confusing, but don't worry—we can take it one step at a time. ";
                default:
                    // If the mood is Neutral or Happy, we don't need a special intro message
                    return "";
            }
        }
    }
}
