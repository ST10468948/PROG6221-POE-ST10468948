using System;
using System.Collections.Generic;

namespace CybersecurityChatbot
{
    public class TaskManager
    {
        
        private readonly TaskStorageHelper _storage;

       
        public TaskManager()
        {
           
            _storage = new TaskStorageHelper();
        }

      
        public string AddTask(string title, string description, string reminder)
        {
            
            _storage.AddTask(title, description, reminder);


            return $"Task added successfully with the description '{description}'.";
        }

    
        public List<CyberTask> GetAllTasks()
        {
            
            return _storage.LoadTasks(); //
        }

      
        public void MarkAsComplete(int id)
        {
           
            _storage.MarkAsComplete(id);

           
        }

     
        public void DeleteTask(int id)
        {
            
            _storage.DeleteTask(id);

            
        }
    }
}