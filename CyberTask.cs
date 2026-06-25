using System;

using System;

namespace CybersecurityChatbot
{
    // This is a plain data class (a model) used to describe what a single task looks like in our program
    public class CyberTask
    {
        // A unique ID number to help the computer tell this specific task apart from others
        public int Id { get; set; }

        // The short main name or title of the task (e.g., "Enable 2FA")
        public string Title { get; set; }

        // Extra details explaining what needs to be done for the task
        public string Description { get; set; }

        // Stores when or how the user wants to be reminded (e.g., "In 3 days")
        public string Reminder { get; set; }

        // A true/false flag to check if the task is finished (true) or still pending (false)
        public bool IsComplete { get; set; }

        // Stores the exact date and time when this task was first created
        public string CreatedAt { get; set; }
    }
}