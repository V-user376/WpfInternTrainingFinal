using Login_Application_12.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace Login_Application_12.ViewModels
{
    internal class MainViewModel
    {
        public MainViewModel() 
        {
            LoginCommand = new RelayCommand(Login);
        }

        public ICommand LoginCommand { get; }

        private void Login()
        {

        }

    }
}
