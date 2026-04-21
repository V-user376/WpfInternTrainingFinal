using Navigation_Application.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace Navigation_Application.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private object currentView;

        public object CurrentView
        {
            get => currentView;
            set
            {
                currentView = value;
                OnPropertyChanged(nameof(CurrentView));

            }
        }

        public ICommand HomeCommand { get; set; }
        public ICommand SettingsCommand { get; set; }
        public ICommand ProductCommand {  get; set; }

        public MainViewModel()
        {
            HomeCommand = new RelayCommand(() => CurrentView = new HomeViewModel());
            SettingsCommand = new RelayCommand(() => CurrentView = new SettingsViewModel());
            ProductCommand = new RelayCommand(() => CurrentView = new ProductViewModel());

            CurrentView = new HomeViewModel();
        }
        public event PropertyChangedEventHandler PropertyChanged;
            
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


    }
}
