using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Task_Management_Application.Commands;
using Task_Management_Application.Models;
using Task_Management_Application.Services;
using Task_Management_Application.ViewModels;

public class TaskDetailViewModel : ViewModelBase
{
    private readonly IDataService dataService;


    private ObservableCollection<Task> allTasks;


    public ObservableCollection<Task> Tasks { get; set; }



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


    public enum TaskStatusFilter
    {
        All,
        Active,
        Completed
    }

    public enum DueDateFilter
    {
        All,
        Today,
        ThisWeek,
        Overdue
    }

    public Array Priorities => Enum.GetValues(typeof(TaskPriority));

    public Array StatusOptions => Enum.GetValues(typeof(TaskStatusFilter));
    public Array PriorityOptions => Enum.GetValues(typeof(TaskPriority));
    public Array DueDateOptions => Enum.GetValues(typeof(DueDateFilter));

    private TaskStatusFilter selectedStatus = TaskStatusFilter.All;

    public TaskStatusFilter SelectedStatus
    {
        get => selectedStatus;
        set
        {
            selectedStatus = value;
            OnPropertyChanged();
            ApplyFilters(); // auto filter
        }
    }

    private TaskPriority? selectedPriority;
    public TaskPriority? SelectedPriority
    {
        get => selectedPriority;
        set
        {
            selectedPriority = value;
            OnPropertyChanged();
            ApplyFilters();
        }
    }

    private DueDateFilter selectedDueDate = DueDateFilter.All;
    public DueDateFilter SelectedDueDate
    {
        get => selectedDueDate;
        set
        {
            selectedDueDate = value;
            OnPropertyChanged();
            ApplyFilters();
        }
    }

    private int totalTasks;
    public int TotalTasks
    {
        get => totalTasks;
        set
        {
            totalTasks = value;
            OnPropertyChanged();
        }
    }

    private int pendingTasks;
    public int PendingTasks
    {
        get => pendingTasks;
        set
        {
            pendingTasks = value;
            OnPropertyChanged();
        }
    }

    private int overdueTasks;
    public int OverdueTasks
    {
        get => overdueTasks;
        set
        {
            overdueTasks = value;
            OnPropertyChanged();
        }
    }






    public ICommand SearchCommand { get; set; }
    public ICommand CheckCommand { get; set; }

    public TaskDetailViewModel()
    {
        //SearchCommand = new RelayCommand(Search);



        dataService = new JsonDataService();

        var list = dataService.LoadTasks();

        allTasks = new ObservableCollection<Task>(list);

        Tasks = new ObservableCollection<Task>(allTasks);

        SearchCommand = new RelayCommand(() =>
        {
            MessageBox.Show("Search command fired");
            Search();
        });

        CheckCommand = new RelayCommand(Check);

        DashboardDetails();
    }

    private void Check()
    {
        MessageBox.Show("clicked");
        throw new NotImplementedException();
    }



    //  Search Logic 
    private void Search()
    {

        //MessageBox.Show("Search clicked");        

        if (string.IsNullOrWhiteSpace(SearchText))
        {
            Tasks.Clear();

            foreach (var task in allTasks)
                Tasks.Add(task);

            Message = "";
            return;
        }

        var result = allTasks
            .Where(t =>
                (!string.IsNullOrEmpty(t.Title) &&
                 t.Title.ToLower().Contains(SearchText.ToLower())) ||

                (!string.IsNullOrEmpty(t.Description) &&
                 t.Description.ToLower().Contains(SearchText.ToLower()))
            )
            .ToList();

        Tasks.Clear();

        if (result.Any())
        {
            foreach (var task in result)
                Tasks.Add(task);

            Message = "";
        }
        else
        {
            Message = "No data found";
        }



    }
    private void ApplyFilters()
    {
        var filtered = allTasks.AsEnumerable();

        // STATUS FILTER
        if (SelectedStatus == TaskStatusFilter.Active)
            filtered = filtered.Where(t => !t.IsCompleted);

        else if (SelectedStatus == TaskStatusFilter.Completed)
            filtered = filtered.Where(t => t.IsCompleted);

        // PRIORITY FILTER
        if (SelectedPriority != null)
            filtered = filtered.Where(t => t.Priority == SelectedPriority);

        // DUE DATE FILTER
        if (SelectedDueDate == DueDateFilter.Today)
        {
            var today = DateTime.Today;
            filtered = filtered.Where(t => t.DueDate.Date == today);
        }
        else if (SelectedDueDate == DueDateFilter.ThisWeek)
        {
            var start = DateTime.Today;
            var end = start.AddDays(7);
            filtered = filtered.Where(t => t.DueDate >= start && t.DueDate <= end);
        }
        else if (SelectedDueDate == DueDateFilter.Overdue)
        {
            var today = DateTime.Today;
            filtered = filtered.Where(t => t.DueDate < today && !t.IsCompleted);
        }

        // UPDATE UI
        Tasks.Clear();

        foreach (var task in filtered)
            Tasks.Add(task);
    }

    private void DashboardDetails()
    {
        var today = DateTime.Today;

        TotalTasks = allTasks.Count();

        PendingTasks = allTasks.Count(t => !t.IsCompleted);

        OverdueTasks = allTasks.Count(t => t.DueDate.Date < today);
    }
}
