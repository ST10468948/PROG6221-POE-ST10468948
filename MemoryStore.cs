using System;

namespace ChatBotGui
{
    public class MemoryStore
    {
        
        public string UserName { get; set; } = "";
        public string FavouriteTopic { get; set; } = "";

        
        public void Store(string key, string value)
        {
            if (key.ToLower() == "name")
            {
                UserName = value;
            }
            else if (key.ToLower() == "topic")
            {
                FavouriteTopic = value;
            }
        }

        
        public string GetPersonalisedOpener()
        {
            if (!string.IsNullOrEmpty(FavouriteTopic))
            {
                return $"As someone interested in {FavouriteTopic}, ";
            }
            return "";
        }
    }
}