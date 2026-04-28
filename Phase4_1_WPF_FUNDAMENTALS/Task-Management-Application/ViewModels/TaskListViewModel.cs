using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Task_Management_Application.Commands;
using Task_Management_Application.Models;
using Task_Management_Application.Services;

namespace Task_Management_Application.ViewModels
{
    public class TaskListViewModel : ViewModelBase
    {
        private readonly IDataService dataService;

        // 🔹 Collection (binds to DataGrid)
        public ObservableCollection<Task> Tasks { get; set; }

        // 🔹 Input field
        private string newTask;
        public string NewTask
        {
            get => newTask;
            set
            {
                newTask = value;
                OnPropertyChanged();
            }
        }

        // 🔹 Commands
        public ICommand AddTaskCommand { get; }
        public ICommand DeleteTaskCommand { get; }

        // 🔹 Constructor
        public TaskListViewModel()
        {
            dataService = new JsonDataService();

            Tasks = new ObservableCollection<Task>();

            AddTaskCommand = new RelayCommand(AddTask);
            DeleteTaskCommand = new RelayCommand(DeleteTask);

            LoadTasks(); // 🔥 load from JSON
        }

        // 🔹 Add Task
        private void AddTask()
        {
            if (!string.IsNullOrWhiteSpace(NewTask))
            {
                Tasks.Add(new Task
                {
                    Title = NewTask
                });

                NewTask = ""; // clear textbox

                SaveTasks(); // optional: auto-save
            }
        }

        // 🔹 Delete Task
        private void DeleteTask()
        {
            //if (parameter is Task task)
            //{
            //    Tasks.Remove(task);
            //    SaveTasks(); // optional: auto-save
            //}
        }

        // 🔹 Load from JSON
        private void LoadTasks()
        {
            var tasks = dataService.LoadTasks();

            Tasks.Clear();

            foreach (var task in tasks)
            {
                Tasks.Add(task);
            }
        }

        // 🔹 Save to JSON
        private void SaveTasks()
        {
            var list = Tasks.ToList();
            dataService.SaveTasks(list);
        }
    }
}
