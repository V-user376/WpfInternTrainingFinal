using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace DataBindingBasics.Models
{
    public class Product : INotifyPropertyChanged
    {
        private int id;
        private string name;
        private int price;
        private int quantity;
        private int totalvalue;

        public int Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public int Price
        {
            get => price;
            set
            {
                

                
                    price = value;
                    
                    OnPropertyChanged(nameof(Price));
                    OnPropertyChanged(nameof(TotalValue));

                


            }
        }
        
        public int Quantity
        {
            get => quantity;
            set
            {               
                quantity = value;               

                OnPropertyChanged(nameof(Quantity));
                OnPropertyChanged(nameof(TotalValue));
            }
        }

        public int TotalValue
        {
            get => Quantity*Price;
            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }        
    }
}
