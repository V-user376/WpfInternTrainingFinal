using System;
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

        
        public ObservableCollection<Task> Tasks { get; set; }

        
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
        private string description;
        public string Description
        {
            get => description;
            set
            {
                description = value;
                OnPropertyChanged();

            }
        }
        private DateTime dueDate = DateTime.Now;
        public DateTime DueDate
        {
            get => dueDate;
            set
            {
                dueDate = value;
                OnPropertyChanged();

            }
        }

        public Array Priorities => Enum.GetValues(typeof(TaskPriority));

        private TaskPriority selectedPriority;
        public TaskPriority SelectedPriority
        {
            get => selectedPriority;
            set
            {
                selectedPriority = value;
                OnPropertyChanged();


            }            
        }

        private Task selectedTask;
        public Task SelectedTask
        {
            get => selectedTask;
            set
            {
                selectedTask = value;
                OnPropertyChanged();

                if(selectedTask != null)
                {
                    NewTask = selectedTask.Title;
                    Description = selectedTask.Description;
                    DueDate = selectedTask.DueDate;
                    SelectedPriority = selectedTask.Priority;
                }
            }
        }



        public ICommand AddTaskCommand { get; }
        public ICommand DeleteTaskCommand { get; }
        public ICommand UpdateTaskCommand { get; }

        
        public TaskListViewModel()
        {
            dataService = new JsonDataService();

            Tasks = new ObservableCollection<Task>();

            AddTaskCommand = new RelayCommand(AddTask);
            DeleteTaskCommand = new RelayCommand(DeleteTask);

            LoadTasks();

            UpdateTaskCommand = new RelayCommand(UpdateTask);
        }

        
        private void AddTask()
        {
            if (!string.IsNullOrWhiteSpace(NewTask))
            {
                Tasks.Add(new Task
                {
                    Title = NewTask,
                    Description = Description,
                    DueDate = dueDate, 
                    Priority = SelectedPriority
                });

                NewTask = "";
                Description = "";
                DueDate = DateTime.Now;


                // clear textbox

                SaveTasks(); // optional: auto-save
            }
        }

        
        private void DeleteTask()
        {
            //if (parameter is Task task)
            //{
            //    Tasks.Remove(task);
            //    SaveTasks(); // optional: auto-save
            //}

            if(SelectedTask != null)
            {
                Tasks.Remove(SelectedTask);

                SaveTasks();

                SelectedTask = null;
            }
        }

        
        private void LoadTasks()
        {
            var tasks = dataService.LoadTasks();

            Tasks.Clear();

            foreach (var task in tasks)
            {
                Tasks.Add(task);
            }
        }

        
        private void SaveTasks()
        {
            var list = Tasks.ToList();
            dataService.SaveTasks(list);
        }

        private void UpdateTask()
        {
            if (SelectedTask != null)
            {
                SelectedTask.Title = NewTask;
                SelectedTask.Description = Description;
                SelectedTask.DueDate = DueDate;
                SelectedTask.Priority = SelectedPriority;

                OnPropertyChanged(nameof(Tasks));

                SaveTasks();
            }
        }


    }
}
