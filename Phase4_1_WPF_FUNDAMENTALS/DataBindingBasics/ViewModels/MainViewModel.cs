using DataBindingBasics.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBindingBasics.Helpers;
using System.Windows.Input;
using System.Windows;

namespace DataBindingBasics.ViewModels
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Product> Products { get; set; }

        public int dataCount;

        private Product selectedProduct;
        public Product SelectedProduct
        {
            get => selectedProduct;
            set
            {
                selectedProduct = value;
                OnPropertyChanged(nameof(SelectedProduct)); // important

                (RemoveProductCommand as RelayCommand)?.RaiseCanExecuteChanged();
            }
        }

        public MainViewModel()
        {
            Products = new ObservableCollection<Product>();

            Products.Add(new Product { Id = 1, Name = "Laptop", Price = 50000, Quantity = 10 });
            Products.Add(new Product { Id = 2, Name = "Phone", Price = 20000, Quantity = 5 });

            AddProductCommand = new RelayCommand(AddProduct);
            RemoveProductCommand = new RelayCommand(RemoveProduct, CanRemoveProduct);

            //OnClickNameCommand = new RelayCommand(AutoGenerateID);

            dataCount = Products.Count();

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        
        private void AddProduct()
        {
            //MessageBox.Show($"{ dataCount}");

            if (selectedProduct != null)
            {
                selectedProduct.Id = ++dataCount;
            }
        }
         
        public Product NewProduct { get; set; } = new Product();

        public ICommand AddProductCommand { get; }

        public ICommand RemoveProductCommand { get; }

        public ICommand OnClickNameCommand { get; set; } 

        private void RemoveProduct()
        {
            if (SelectedProduct != null)
            {
                Products.Remove(SelectedProduct);
            }
        }

        private bool CanRemoveProduct()
        {
            return SelectedProduct != null;
        }

        //private void AutoGenerateID()
        //{
        //    NewProduct.Id = dataCount++;
        //}

    }
}
