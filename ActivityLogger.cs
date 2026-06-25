using System;
using System.Collections.Generic;

namespace ChatBotGui
{
    // This class keeps track of a running list of actions that happen in our app
    public class ActivityLogger
    {
        // A static list to store all our log strings so they stay in memory throughout the app's life
        private static List<string> _logEntries = new List<string>();

        /// <summary>
        /// Adds a brand new timestamped message to our activity log history
        /// </summary>
        public static void Log(string actionDescription)
        {
            // Get the current system time and format it nicely as [HH:mm] (e.g., [14:35])
            string timestamp = DateTime.Now.ToString("[HH:mm] ");

            // Combine the timestamp with the action message
            string completeEntry = timestamp + actionDescription;

            // Add the combined text into our tracking list collection
            _logEntries.Add(completeEntry);
        }

        /// <summary>
        /// Returns a formatted numbered string containing the most recent 5 to 10 entries
        /// </summary>
        public static string GetRecentLog(int count = 10)
        {
            // If our log list is completely empty, give back a simple warning statement
            if (_logEntries.Count == 0)
            {
                return "The activity log is currently empty.";
            }

            // Figure out where to start copying from so we only grab the latest entries
            int total = _logEntries.Count;
            int startingIndex = total - count;

            // If we have fewer total entries than the requested count, start at 0
            if (startingIndex < 0)
            {
                startingIndex = 0;
            }

            string resultText = "Here's a summary of recent actions:\n";
            int displayNumbers = 1;

            // Loop starting from our calculation index all the way to the end of the collection
            for (int i = startingIndex; i < total; i++)
            {
                resultText += $"{displayNumbers}. {_logEntries[i]}\n";
                displayNumbers++;
            }

            // Inform the user if there are more entries hidden behind the visual cutoff wall
            if (total > count)
            {
                resultText += "\n(There are more actions saved. Type 'show more' to see the full log history.)";
            }

            return resultText;
        }

        /// <summary>
        /// Returns a numbered list string containing absolutely every single log entry saved
        /// </summary>
        public static string GetFullLog()
        {
            if (_logEntries.Count == 0)
            {
                return "The activity log is currently empty.";
            }

            string resultText = "Here's the complete activity log history:\n";
            int displayNumbers = 1;

            // Loop through every single entry stored from the beginning
            foreach (string entry in _logEntries)
            {
                resultText += $"{displayNumbers}. {entry}\n";
                displayNumbers++;
            }

            return resultText;
        }

        /// <summary>
        /// Returns the total count of items currently in the log
        /// </summary>
        public static int GetLogCount()
        {
            return _logEntries.Count;
        }
    }
}