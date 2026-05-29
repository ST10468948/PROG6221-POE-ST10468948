using System;

namespace ChatBotGui
{
    public class MemoryStore
    {
        // Variables to hold onto the user's name and the topic they like most
        public string UserName { get; set; } = "";
        public string FavouriteTopic { get; set; } = "";

        // This method decides which "slot" to save information into
        public void Store(string key, string value)
        {
            // If the key is 'name', save the value to the UserName slot
            if (key.ToLower() == "name")
            {
                UserName = value;
            }
            // If the key is 'topic', save it to FavouriteTopic instead
            else if (key.ToLower() == "topic")
            {
                FavouriteTopic = value;
            }
        }

        // This creates a custom sentence starter if the bot knows what you like
        public string GetPersonalisedOpener()
        {
            // If we have a favorite topic saved, use it in a sentence
            if (!string.IsNullOrEmpty(FavouriteTopic))
            {
                return $"As someone interested in {FavouriteTopic}, ";
            }
            // If we don't know a favorite topic yet, just return nothing
            return "";
        }
    }
}