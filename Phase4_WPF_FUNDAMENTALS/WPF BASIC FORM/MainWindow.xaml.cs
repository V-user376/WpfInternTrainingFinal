using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using System.Drawing;

namespace WPF_BASIC_FORM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            string userName = namefield.Text;
            string email = emailfield.Text;
            string phone = phonefield.Text;
            string address = addressfield.Text;

            //MessageBox.Show($"{userName} {email} {phone} {address} data submitted");

            if(!IsValidEmail(email))
            {
                MessageBox.Show("Enter a valid email address", "Invalid Email", MessageBoxButton.OK, MessageBoxImage.Warning);
                emailfield.Focus();
                return;
            }

            if(!IsValidPhone(phone))
            {
                MessageBox.Show("Please enter a valid 10 digit phone number", "Invalid Phone", MessageBoxButton.OK, MessageBoxImage.Warning);
                phonefield.Focus(); return;
            }


            MessageBox.Show($"{userName} {email} {phone} {address} data submitted");

        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            namefield.Clear();
            emailfield.Clear();
            phonefield.Clear();
            addressfield.Clear();
        }


        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email,pattern);

        }

        private bool IsValidPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return false;

            if (phone.Length != 10)
                return false;
            foreach(char c in phone)
            {
                if(!char.IsDigit(c))
                    return false;
            }
            return true;
        }
    }
}