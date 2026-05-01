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
//            dataService.Savex(list);
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



using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Task_Management_Application.Commands;
using Task_Management_Application.Views;
using System.Windows.Forms;
using System.Drawing;
using System.IO; 


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


        private bool isDarkTheme;

        public bool IsDarkTheme
        {
            get => isDarkTheme;
            set
            {
                if (isDarkTheme != value)
                {
                    isDarkTheme = value;
                    OnPropertyChanged();

                    ApplyTheme();
                    SaveTheme();
                }
            }
        }




        private bool dashboardVisible;
        public bool DashboardVisible
        {
            get => dashboardVisible;
            set
            {
                if (dashboardVisible != value)
                {
                    dashboardVisible = value; 
                    OnPropertyChanged();
                }
            }
        }

        private DispatcherTimer notificationTimer;

        
        public ICommand ShowTaskListCommand { get; }
        public ICommand ShowTaskDetailCommand { get; }
        public ICommand ShowSettingsCommand { get; }
        public TaskListViewModel TaskListVM { get; set; }

        

        private TaskListViewModel taskListVM = new TaskListViewModel();
        private TaskDetailViewModel taskDetailVM = new TaskDetailViewModel();

        public MainViewModel()
        {
            
            ShowTaskListCommand = new RelayCommand(ShowTaskList);
            ShowTaskDetailCommand = new RelayCommand(ShowTaskDetail);
            ShowSettingsCommand = new RelayCommand(ShowSettings);


            TaskListVM = new TaskListViewModel();

            CurrentView = new TaskListView
            {
                DataContext = taskListVM
            };

            notificationTimer = new DispatcherTimer();
            notificationTimer.Interval = TimeSpan.FromSeconds(5);
            notificationTimer.Tick += CheckDueTasks;
            notificationTimer.Start();

            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = SystemIcons.Information;
            notifyIcon.Visible = true;
            notifyIcon.Text = "Text Manager";


            LoadTheme();
            ApplyTheme();
            
        }

        private DateTime lastNotificationDate = DateTime.MinValue;

        private NotifyIcon notifyIcon;
        
        private void ShowTaskList()
        {
            CurrentView = new TaskListView();
            //{
            //    DataContext = taskListVM
            //};
        }

        private void ShowTaskDetail()
        {
            CurrentView = new TaskDetailView();
            //{
            //    DataContext = taskDetailVM
            //};
        }

        private void ShowSettings()
        {
            //System.Windows.MessageBox.Show("Settings clicked");


            CurrentView = new SettingsView
            {
                DataContext = this
            };

            //System.Windows.MessageBox.Show(CurrentView.GetType().Name);
        }

        private void CheckDueTasks(object sender, EventArgs e)
        {
            var today = DateTime.Today;

            if (lastNotificationDate == today)
                return;

            var tasks = taskListVM.Tasks;

            var dueTodayTasks = tasks
                .Where(t => t.DueDate.Date == today && !t.IsCompleted)
                .ToList();

            if (dueTodayTasks.Any())
            {
                string message = "Tasks due today:\n";

                foreach (var task in dueTodayTasks)
                {
                    message += "- " + task.Title + "\n";
                }

                ShowNotification(message); // USE BALLOON

                lastNotificationDate = today;
            }
        }


        private void ShowNotification(string message)
        {
            notifyIcon.BalloonTipTitle = "Task Reminder";
            notifyIcon.BalloonTipText = message;
            notifyIcon.BalloonTipIcon = ToolTipIcon.Info;

            notifyIcon.ShowBalloonTip(4000);
        }

        private void ShowDashboard()
        {
            if (!DashboardVisible)
                return;

            CurrentView = new TaskDetailView();
        }

        private void ApplyTheme()
        {
            var theme = new ResourceDictionary();

            if (IsDarkTheme)
            {
                theme.Source = new Uri("Themes/DarkTheme.xaml", UriKind.Relative);
            }
            else
            {
                theme.Source = new Uri("Themes/LightTheme.xaml", UriKind.Relative);
            }

            System.Windows.Application.Current.Resources.MergedDictionaries.Clear();
            System.Windows.Application.Current.Resources.MergedDictionaries.Add(theme);
        }


        private void SaveTheme()
        {
            File.WriteAllText("theme.txt", IsDarkTheme.ToString());
        }


        private void LoadTheme()
        {
            if (File.Exists("theme.txt"))
            {
                var value = File.ReadAllText("theme.txt");

                if (bool.TryParse(value, out bool result))
                {
                    IsDarkTheme = result;
                }
            }
        }


    }
}
