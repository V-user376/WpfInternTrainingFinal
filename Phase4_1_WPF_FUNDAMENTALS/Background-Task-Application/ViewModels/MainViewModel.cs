using Background_Task_Application.Commands;
using Background_Task_Application.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Input;
using System.Windows;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Threading;


namespace Background_Task_Application.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public ICommand InsertDataCommand { get; set; }


        public MainViewModel()
        {
            InsertDataCommand = new RelayCommand(_ => GenerateData());
            CancelCommand = new RelayCommand(_ => CancelTask());
            PauseCommand = new RelayCommand(_ => PauseTask());
            ResumeCommand = new RelayCommand(_ => ContinueTask());
        }

        private int progress;
        public int Progress
        {
            get { return progress; }
            set
            {
                progress = value;
                OnPropertyChanged(nameof(Progress));
            }
        }

        private CancellationTokenSource _cts;

        public ICommand CancelCommand { get; set; }

        public ICommand PauseCommand { get; set; }
        public ICommand ResumeCommand { get; set; }

        private ManualResetEventSlim pauseEvent = new ManualResetEventSlim(true);
        

        private async void GenerateData()
        {

            //MessageBox.Show("Data begins to insert in JSON file");
            _cts = new CancellationTokenSource();
            CancellationToken token = _cts.Token;

            bool isCancel = false;

            await Task.Run(() =>

            {
                var jsonData = new List<Product>();
                var random = new Random();
                string[] names = { "user" };

                for (int i = 1; i <= 1000; i++)
                {
                    pauseEvent.Wait();

                    if (token.IsCancellationRequested)
                    {
                        isCancel = true;                        
                        return;
                    }

                    jsonData.Add(new Product
                    {
                        Id = i,
                        Name = "user" + i
                    });

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Progress = i * 100 / 1000;
                    });

                    System.Threading.Thread.Sleep(100);

                }

                string insertData = JsonConvert.SerializeObject(jsonData, Formatting.Indented);


                File.WriteAllText("data.json", insertData);

            }, token);


            if(isCancel)
            {
                Progress = 0;                 
            }
            

        //MessageBox.Show("Data Successfully inserted");
        }
        private void CancelTask()
        {
            _cts.Cancel();
        }

        private void PauseTask()
        {
            pauseEvent.Reset();
        }
        private void ContinueTask()
        {
            pauseEvent.Set();
        }


        //private void GenerateDataSyc()
        //{
        //    MessageBox.Show("Sync process started");

        //    var jsonData = new List<Product>();

        //    for (int i = 1; i <= 1000; i++)
        //    {
        //        jsonData.Add(new Product
        //        {
        //            Id = i,
        //            Name = "User " + i
        //        });

        //        // Simulate heavy work (2 seconds per iteration)
        //        System.Threading.Thread.Sleep(50);
        //    }

        //    string insertData = JsonConvert.SerializeObject(jsonData, Formatting.Indented);

        //    File.WriteAllText("data_sync.json", insertData);

        //    MessageBox.Show("Sync process completed");
        //}

        //private CancellationTokenSource _cts;

        //public ICommand CancelCommand { get; set; }

    


    };
}
