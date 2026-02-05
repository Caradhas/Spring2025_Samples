using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Library.eCommerce.Models;
using Library.eCommerce.Services;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Spring2025_Samples.Models;

namespace Maui.eCommerce.ViewModels
{
    public class InventoryManagementViewModel : INotifyPropertyChanged
    {
        public int sender = 0;
        public Item? SelectedProduct { get; set; }
        public string? Query { get; set; }
        private ProductServiceProxy _svc = ProductServiceProxy.Current;
        public event PropertyChangedEventHandler? PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if(propertyName is null){
                throw new ArgumentNullException(nameof(propertyName));
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public ObservableCollection<Item?> Products
        {
            get
            {
                var filteredList = _svc.Products.Where(p => p?.Quantity > 0 && (p?.Product?.Name?.ToLower().Contains(Query?.ToLower() ?? string.Empty) ?? false)); 
                if (sender == 2)
                {
                    filteredList = _svc.Products.Where(p => p?.Quantity > 0 && (p?.Product?.cost?.ToString()?.Contains(Query ?? string.Empty) ?? false));
                }
                return new ObservableCollection<Item?>(filteredList);
            }
        }
        public Item? Delete()
        {
            var item = _svc.Delete(SelectedProduct?.Id ?? 0);
            NotifyPropertyChanged("Products");

            return item;
        }
        public void RefreshProductList1()
        {
            sender = 1;

            NotifyPropertyChanged(nameof(Products));
        }
        public void RefreshProductList2()
        {
            sender = 2;

            NotifyPropertyChanged(nameof(Products));
        }
        public void RefreshProductList()
        {
            NotifyPropertyChanged(nameof(Products));
        }













    }
}

