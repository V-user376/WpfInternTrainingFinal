using Background_Task_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Newtonsoft.Json;
using Background_Task_Application.Models;
using Background_Task_Application.ViewModels;
using Background_Task_Application.Commands;

namespace Background_Task_Application
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainViewModel();

            

        }

        //private void GenerateData()
        //{
        //    var jsonData = new List<Product>();
        //    var random = new Random();
        //    string[] names = { "user" };

        //    for(int i = 1; i <= 1000; i++)
        //    {
        //        jsonData.Add(new Product
        //        {
        //            Id = i,
        //            Name = "user" + i
        //        });
        //    }

        //    string insertData = JsonConvert.SerializeObject(jsonData, Formatting.Indented);
            

        //    File.WriteAllText("data.json", insertData);
        //}
    }
}
