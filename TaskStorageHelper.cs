using System;
using System.Collections.Generic;
using System.IO;                  
using Newtonsoft.Json;            

namespace CybersecurityChatbot
{
    public class TaskStorageHelper
    {
        
        private const string FilePath = "tasks.json";

        
        public List<CyberTask> LoadTasks()
        {
            try
            {
             
                if (!File.Exists(FilePath))
                {
                    return new List<CyberTask>();
                }

                
                string jsonText = File.ReadAllText(FilePath);

                 
                List<CyberTask> tasksList = JsonConvert.DeserializeObject<List<CyberTask>>(jsonText);

                
                if (tasksList == null)
                {
                    return new List<CyberTask>();
                }

                return tasksList;
            }
            catch (Exception ex)
            {
                
                Console.WriteLine("Error loading tasks: " + ex.Message);
                return new List<CyberTask>();
            }
        }

     
        public void SaveTasks(List<CyberTask> tasks)
        {
            try
            {
                
                string jsonText = JsonConvert.SerializeObject(tasks, Formatting.Indented);

                
                File.WriteAllText(FilePath, jsonText);
            }
            catch (Exception ex)
            {
              
                Console.WriteLine("Error saving tasks: " + ex.Message);
            }
        }


        public void AddTask(string title, string description, string reminder)
        {
           
            List<CyberTask> currentTasks = LoadTasks();

           
            int newId = 1;
            if (currentTasks.Count > 0)
            {
              
                newId = currentTasks[currentTasks.Count - 1].Id + 1;
            }

           
            CyberTask newTask = new CyberTask()
            {
                Id = newId,
                Title = title,
                Description = description,
                Reminder = reminder,
                IsComplete = false, 
                CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm")
            };

            
            currentTasks.Add(newTask);

           
            SaveTasks(currentTasks);
        }

    
        public void MarkAsComplete(int id)
        {
            
            List<CyberTask> currentTasks = LoadTasks();

           
            foreach (CyberTask task in currentTasks)
            {
                if (task.Id == id)
                {
                    
                    task.IsComplete = true;
                    break; 
                }
            }

           
            SaveTasks(currentTasks);
        }

       
        public void DeleteTask(int id)
        {
            
            List<CyberTask> currentTasks = LoadTasks();

            
            CyberTask taskToRemove = null;

           
            foreach (CyberTask task in currentTasks)
            {
                if (task.Id == id)
                {
                    taskToRemove = task;
                    break;
                }
            }

            
            if (taskToRemove != null)
            {
                currentTasks.Remove(taskToRemove);
            }

           
            SaveTasks(currentTasks);
        }
    }
}