using CybersecurityChatbot;
using System;
using System.Collections.Generic;

namespace ChatBotGui
{
    // This class handles the business logic for managing tasks, acting as a bridge between the GUI and storage
    public class TaskManager
    {
        // Create a private variable to hold our storage helper object so we can use it throughout this class
        private readonly TaskStorageHelper _storage;

        // Constructor method that runs automatically when a new TaskManager object is made
        public TaskManager()
        {
            // Initialize our storage helper object so it is ready to read and write files
            _storage = new TaskStorageHelper();
        }

        // Method to add a new task by taking in its title, description, and reminder details
        public string AddTask(string title, string description, string reminder)
        {
            // Pass the details to our storage helper so it can save the new task into the JSON file
            _storage.AddTask(title, description, reminder);

            // Add an entry to our activity log history when a task is successfully created
            ActivityLogger.Log($"Task added: '{title}' (Reminder set for {reminder})");

            // Return a friendly confirmation message text back to the user interface
            return $"Task added successfully with the description '{description}'.";
        }

        // Method that retrieves and returns the entire list of tasks saved in our system
        public List<CyberTask> GetAllTasks()
        {
            // Ask the storage helper to read the JSON file and send back the list of tasks
            return _storage.LoadTasks();
        }

        // Method to mark a specific task as done by using its unique ID number
        public void MarkAsComplete(int id)
        {
            // Tell the storage helper to find this specific ID and change its completed status to true
            _storage.MarkAsComplete(id);

            // Add an entry to our activity log history when a task is updated to done
            ActivityLogger.Log($"Task marked complete (ID: {id})");
        }

        // Method to completely remove a task from our system using its unique ID number
        public void DeleteTask(int id)
        {
            // Tell the storage helper to find this specific ID and wipe it out of the JSON file
            _storage.DeleteTask(id);

            // Add an entry to our activity log history when a task is deleted
            ActivityLogger.Log($"Task deleted (ID: {id})");
        }
    }
}