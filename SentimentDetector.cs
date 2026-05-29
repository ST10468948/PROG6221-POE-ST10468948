using System;
using System.Collections.Generic;

namespace ChatBotGui
{
    public enum Sentiment { Neutral, Worried, Curious, Frustrated, Happy } 

    public class SentimentDetector
    {
        private Dictionary<Sentiment, List<string>> triggerWords;

        public SentimentDetector()
        {
            triggerWords = new Dictionary<Sentiment, List<string>>();

            triggerWords.Add(Sentiment.Worried, new List<string> { "worried", "scared", "afraid", "anxious", "unsafe" });
            triggerWords.Add(Sentiment.Curious, new List<string> { "curious", "wondering", "interested", "how does" });
            triggerWords.Add(Sentiment.Frustrated, new List<string> { "frustrated", "annoyed", "confused", "don't understand" });
        }

        public Sentiment Detect(string input)
        {
            string lowInput = input.ToLower();
            foreach (var pair in triggerWords)
            {
                foreach (string word in pair.Value)
                {
                    if (lowInput.Contains(word))
                    {
                        return pair.Key;
                    }
                }
            }
            return Sentiment.Neutral;
        }

        public string GetSentimentResponse(Sentiment s)
        {
            switch (s)
            {
                case Sentiment.Worried:
                    return "It is completely understandable to feel anxious about online security. Let's tackle it safely. ";
                case Sentiment.Curious:
                    return "I am glad you are exploring this topic! Knowledge is your best shield. ";
                case Sentiment.Frustrated:
                    return "Tech can be incredibly confusing, but don't worry—we can take it one step at a time. ";
                default:
                    return ""; 
            }
        }
    }
}