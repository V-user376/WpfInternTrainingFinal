using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public Settings()
        {
            InitializeComponent();
            FontSizeSlider.ValueChanged += (s, e) =>
            {
                PreviewText.FontSize = FontSizeSlider.Value;
            };
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Resources["AppFontSize"] = FontSizeSlider.Value;

            System.Windows.MessageBox.Show("Font size saved!");

            this.Close();
        }        
    }
}
