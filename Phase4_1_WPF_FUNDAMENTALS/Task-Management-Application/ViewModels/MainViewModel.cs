//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Input;
//using Task_Management_Application.Commands;
//using Task_Management_Application.Services;
//using Task_Management_Application.Services;

//namespace Task_Management_Application.ViewModels
//{
//    public class MainViewModel : ViewModelBase
//    {
//        private string appTitle;

//        public string AppTitle
//        {
//            get => appTitle;
//            set
//            {
//                appTitle = value;
//                OnPropertyChanged();
//            }
//        }

//        new string newTask;
//        public string NewTask
//        {
//            get => newTask;
//            set
//            {
//                newTask = value;
//                OnPropertyChanged();
//            }
//        }



//        private object currentView;
//        public object CurrentView
//        {
//            get => currentView;
//            set
//            {
//                currentView = value;
//                OnPropertyChanged();
//            }
//        }
//        private readonly IDataService dataService;

//        public ICommand TestCommand { get; }
//        public ICommand AddTaskCommand { get; }
//        public ICommand DeleteTaskCommand { get; }

//        public ICommand ShowTaskListCommand { get; }
//        public ICommand ShowSearchCommand { get; }
//        public ICommand ShowTaskDetailCommand { get;}
//        public ICommand ShowSettingsCommand { get; }




//        public MainViewModel()
//        {
//            AppTitle = "Task Management App";

//            TestCommand = new RelayCommand(OnTest);

//            TaskListVM = new TaskListViewModel();

//            AddTaskCommand = new RelayCommand(AddTask);

//            DeleteTaskCommand = new RelayCommand(DeleteTask);

//            dataService = new JsonDataService();

//            LoadTasks();

//            ShowTaskListCommand = new RelayCommand(_ => ShowTaskList());
//            ShowSearchCommand = new RelayCommand(_ => ShowSearch());
//            CurrentView = new Views.TaskListView();  // 🔥 default page

//            ShowTaskDetailCommand = new RelayCommand(_ => ShowTaskDetail());
//            ShowSettingsCommand = new RelayCommand(_ => ShowSettings());

//            CurrentView = new Views.TaskListView();

//        }

//        private void OnTest()
//        {
//            AppTitle = "Button Clicked!";
//        }

//        public TaskListViewModel TaskListVM { get; set; }

//        private void AddTask()
//        {
//            if (!string.IsNullOrWhiteSpace(NewTask))
//            {
//                TaskListVM.Tasks.Add(new Models.Task
//                {
//                    Title = NewTask,
//                    Priority = Models.TaskPriority.Low
//                });
//                NewTask = "";
//            }                       
//        }

//        private void DeleteTask()
//        {
//            //if (parameter is Models.Task task)
//            //{
//            //    //TaskListVM.Tasks;
//            //}
//        }
//        private void LoadTasks()
//        {
//            var tasks = dataService.LoadTasks();
//            TaskListVM.Tasks.Clear();

//            foreach(var task in tasks)
//            {
//                TaskListVM.Tasks.Add(task);
//            }
//        }

//        public void SaveTask()
//        {
//            System.Windows.MessageBox.Show("Saving...");

//            var list = TaskListVM.Tasks.ToList();
//            dataService.SaveTasks(list);
//        }






//        private void ShowTaskList()
//        {
//            CurrentView = new Views.TaskListView();
//        }

//        private void ShowSearch()
//        {
//            CurrentView = new Views.SearchView();
//        }

//        private void ShowTaskDetail()
//        {
//            CurrentView = new Views.TaskDetailView();
//        }
//        private void ShowSettings()
//        {
//            CurrentView = new Views.SettingsView();
//        }
//    }
//}



using System.Windows.Input;
using Task_Management_Application.Commands;
using Task_Management_Application.Views;

namespace Task_Management_Application.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        
        private object currentView;
        public object CurrentView
        {
            get => currentView;
            set
            {
                currentView = value;
                OnPropertyChanged();
            }
        }

        
        public ICommand ShowTaskListCommand { get; }
        public ICommand ShowTaskDetailCommand { get; }
        public ICommand ShowSettingsCommand { get; }

        
        public MainViewModel()
        {
            
            ShowTaskListCommand = new RelayCommand(ShowTaskList);
            ShowTaskDetailCommand = new RelayCommand( ShowTaskDetail);
            ShowSettingsCommand = new RelayCommand( ShowSettings);

            
            CurrentView = new TaskListView();
        }

        
        private void ShowTaskList()
        {
            CurrentView = new TaskListView();
        }

        private void ShowTaskDetail()
        {
            CurrentView = new TaskDetailView();
        }

        private void ShowSettings()
        {
            CurrentView = new SettingsView();
        }
    }
}
