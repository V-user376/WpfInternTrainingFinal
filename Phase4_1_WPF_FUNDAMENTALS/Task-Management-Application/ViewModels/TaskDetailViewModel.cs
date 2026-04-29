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

    // 🔹 Original full data
    private ObservableCollection<Task> allTasks;

    // 🔹 Data shown in DataGrid
    public ObservableCollection<Task> Tasks { get; set; }

    // 🔹 Search text
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

    // 🔹 Message for "No data found"
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

    // 🔹 Command
    public ICommand SearchCommand { get; set; }
    public ICommand CheckCommand { get; set; }

    public TaskDetailViewModel()
    {
        SearchCommand = new RelayCommand(Search);

        CheckCommand = new RelayCommand(Check);

        dataService = new JsonDataService();

        var list = dataService.LoadTasks();

        allTasks = new ObservableCollection<Task>(list);

        Tasks = new ObservableCollection<Task>(allTasks);

        
    }

    private void Check()
    {
        MessageBox.Show("clicked");
        throw new NotImplementedException();
    }

    private void Search()
    {
        throw new NotImplementedException();
    }

    // 🔍 SEARCH LOGIC
    //private void Search()
    //{

    //    MessageBox.Show("Search clicked");

    //    if (string.IsNullOrWhiteSpace(SearchText))
    //    {
    //        Tasks = new ObservableCollection<Task>(allTasks);
    //        Message = "";
    //    }
    //    else
    //    {
    //        var result = allTasks
    //            .Where(t => t.Title.Contains(SearchText)) // case-sensitive
    //            .ToList();

    //        if (result.Any())
    //        {
    //            Tasks = new ObservableCollection<Task>(result);
    //            Message = "";
    //        }
    //        else
    //        {
    //            Tasks.Clear();
    //            Message = "No data found";
    //        }
    //    }

    //    OnPropertyChanged(nameof(Tasks));
    //}
}
