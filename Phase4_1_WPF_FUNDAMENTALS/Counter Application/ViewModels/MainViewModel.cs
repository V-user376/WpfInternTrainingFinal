using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using Counter_Application.Models;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Counter_Application.Helpers;
using System.Windows;
using System.Text.RegularExpressions;

namespace Counter_Application.ViewModels
{
    internal class MainViewModel: INotifyPropertyChanged
    {
        private string numberText;

        // For TextBox input
        public string NumberText
        {
            get => numberText;
            set
            {
                if (!Regex.IsMatch(value, "^[0-9]*$"))
                {
                    MessageBox.Show("Only numbers are allowed");
                    return;
                }

                numberText = value;
                OnPropertyChanged(nameof(NumberText));
                OnPropertyChanged(nameof(Number)); // update int property
            }
        }

        // For calculations / commands
        public int Number
        {
            get => int.TryParse(NumberText, out int result) ? result : 0;
            set
            {
                numberText = value.ToString();
                OnPropertyChanged(nameof(NumberText));
                OnPropertyChanged(nameof(Number));
            }
        }

        public MainViewModel()
        {
            IncreaseNumberCommand = new RelayCommand(IncreaseNumber);
            DecreaseNumberCommand = new RelayCommand(DecreaseNumber);
            ResetNumberCommand = new RelayCommand(ResetNumber); 
        }
        protected void  OnPropertyChanged(string numbers)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(numbers));
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand IncreaseNumberCommand { get; }
        public ICommand DecreaseNumberCommand { get; }
        public ICommand ResetNumberCommand { get; }

        private void IncreaseNumber()
        {
            //Console.WriteLine(Number);
            //MessageBox.Show(Number);
            Number++;
            
        }
        private void DecreaseNumber()
        {
            Number--;
        }

        private void ResetNumber()
        {
            Number = 0;
        }
        public EventHandler CanExecuteChanged;



    }

    


}
