using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_Management_Application.Models;
using Task = Task_Management_Application.Models.Task;
using Newtonsoft.Json;

namespace Task_Management_Application.Services
{
    public class JsonDataService : IDataService
    {
        private readonly string filePath = "tasks.json";

        public List<Task> LoadTasks()
        {
            if (!File.Exists(filePath))
                return new List<Task>();

            var json = File.ReadAllText(filePath);

            return JsonConvert.DeserializeObject<List<Task>>(json)
                   ?? new List<Task>();
        }

        public void SaveTasks(List<Task> tasks)
        {
            var json = JsonConvert.SerializeObject(tasks, Formatting.Indented);

            File.WriteAllText(filePath, json);
        }
    }
}