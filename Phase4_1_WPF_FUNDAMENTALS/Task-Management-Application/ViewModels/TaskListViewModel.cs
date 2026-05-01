using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
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

        private string searchText;
        public string SearchText
        {
            get => searchText;
            set
            {
                searchText = value;
                OnPropertyChanged();
            }
        }


        private string message;
        public string Message
        {
            get => message;
            set
            {
                message = value;
                OnPropertyChanged();
            }
        }

        //public enum TaskStatusFilter
        //{
        //    All,
        //    Active,
        //    Completed
        //}

        //public enum DueDateFilter
        //{
        //    All,
        //    Today,
        //    ThisWeek,
        //    Overdue 
        //}


        public Array Priorities => Enum.GetValues(typeof(TaskPriority));
        
        //public Array StatusOptions => Enum.GetValues(typeof(TaskStatusFilter));
        //public Array PriorityOptions => Enum.GetValues(typeof(TaskPriority));
        //public Array DueDateOptions => Enum.GetValues(typeof(DueDateFilter));



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
        public ICommand SearchCommand { get; set; }
        public ICommand CheckCommand { get; set; }



        public TaskListViewModel()
        {
            dataService = new JsonDataService();

            Tasks = new ObservableCollection<Task>();

            AddTaskCommand = new RelayCommand(AddTask);
            DeleteTaskCommand = new RelayCommand(DeleteTask);

            LoadTasks();

            UpdateTaskCommand = new RelayCommand(UpdateTask);

            SearchCommand = new RelayCommand(Search);
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

            //if(SelectedTask != null)
            //{
            //    Tasks.Remove(SelectedTask);

            //    SaveTasks();

            //    SelectedTask = null;
            //}

            if (SelectedTask == null)
                return;

            var result = MessageBox.Show("Are you sure you want to delete", "Confirm Delete", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

            if (result == MessageBoxResult.OK)
            {
                
                Tasks.Remove(SelectedTask);

                SaveTasks();
            }
            else
            {
                
                return;
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

        private void Search()
        {
            MessageBox.Show("search");
            
        }
    }
}
