using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_Management_Application.Models;
using Task = Task_Management_Application.Models.Task;


namespace Task_Management_Application.Services
{
    public interface IDataService
    {
        List<Task> LoadTasks();
        void SaveTasks(List<Task> tasks);
    }
}
