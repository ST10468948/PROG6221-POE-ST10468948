using System;
using System.Collections.Generic;
using System.IO;                  // Required for file reading and writing operations
using Newtonsoft.Json;            // Required for converting text data into C# lists and vice versa

namespace CybersecurityChatbot
{
    // This helper class handles all reading and writing of our tasks text file on the hard drive
    public class TaskStorageHelper
    {
        // The fixed name of the file where our task data will be stored permanently
        private const string FilePath = "tasks.json";

        // Method to open the text file and read our saved tasks back into a usable C# List
        public List<CyberTask> LoadTasks()
        {
            try
            {
                // If the file does not exist yet, return a blank list so our application does not crash
                if (!File.Exists(FilePath))
                {
                    return new List<CyberTask>();
                }

                // Read all of the plain text stored inside our text file
                string jsonText = File.ReadAllText(FilePath);

                // Convert the plain JSON text format back into a real C# List of tasks
                List<CyberTask> tasksList = JsonConvert.DeserializeObject<List<CyberTask>>(jsonText);

                // If the file was corrupted or completely empty, return a blank list instead of a null value
                if (tasksList == null)
                {
                    return new List<CyberTask>();
                }

                return tasksList;
            }
            catch (Exception ex)
            {
                // If something goes wrong, print the error to the console and return an empty list
                Console.WriteLine("Error loading tasks: " + ex.Message);
                return new List<CyberTask>();
            }
        }

        // Method to convert a list of tasks into JSON text format and save it to the hard drive
        public void SaveTasks(List<CyberTask> tasks)
        {
            try
            {
                // Convert our C# task list into organized JSON text. Indented makes it clean and easy to read
                string jsonText = JsonConvert.SerializeObject(tasks, Formatting.Indented);

                // Write the clean JSON text directly into our file on the disk
                File.WriteAllText(FilePath, jsonText);
            }
            catch (Exception ex)
            {
                // Print a message to the console window if saving the file fails
                Console.WriteLine("Error saving tasks: " + ex.Message);
            }
        }

        // Method to add a brand new task with its title, description, and reminder details
        public void AddTask(string title, string description, string reminder)
        {
            // 1. Read all the tasks currently saved in our text file first
            List<CyberTask> currentTasks = LoadTasks();

            // 2. Set up an ID counter starting at 1 for our new task
            int newId = 1;
            if (currentTasks.Count > 0)
            {
                // Look at the very last task in our list, grab its ID number, and add 1 to it
                newId = currentTasks[currentTasks.Count - 1].Id + 1;
            }

            // 3. Create a brand new task object and populate all of its properties
            CyberTask newTask = new CyberTask()
            {
                Id = newId,
                Title = title,
                Description = description,
                Reminder = reminder,
                IsComplete = false, // All newly created tasks start out as incomplete (false)
                CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm") // Save the exact time it was made
            };

            // 4. Add our newly configured task to our current list container
            currentTasks.Add(newTask);

            // 5. Overwrite the file on the hard drive immediately so our new data is saved securely
            SaveTasks(currentTasks);
        }

        // Method to find a specific task using its ID number and toggle its status to done
        public void MarkAsComplete(int id)
        {
            // Load the full list of saved tasks from the text document
            List<CyberTask> currentTasks = LoadTasks();

            // Loop through every single task inside our list one by one
            foreach (CyberTask task in currentTasks)
            {
                // If we find a task whose ID matches the ID we are looking for
                if (task.Id == id)
                {
                    // Update its completed status flag to true
                    task.IsComplete = true;
                    break; // Stop looking and exit the loop since we found our match
                }
            }

            // Save our updated list back to the text file on disk
            SaveTasks(currentTasks);
        }

        // Method to completely wipe out a task from our text file using its ID number
        public void DeleteTask(int id)
        {
            // Load the current list of saved tasks from our file
            List<CyberTask> currentTasks = LoadTasks();

            // Create a temporary blank variable to hold the task we plan to remove
            CyberTask taskToRemove = null;

            // Loop through all tasks to find the one that matches our target ID
            foreach (CyberTask task in currentTasks)
            {
                if (task.Id == id)
                {
                    // Store the matching task in our temporary variable
                    taskToRemove = task;
                    break; // Exit the loop early
                }
            }

            // If we successfully found a matching task in our loop
            if (taskToRemove != null)
            {
                // Remove that specific task from our main collection list
                currentTasks.Remove(taskToRemove);
            }

            // Save the updated list back to our hard drive without the deleted task
            SaveTasks(currentTasks);
        }
    }
}