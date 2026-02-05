using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Library.eCommerce.Models;
using Library.eCommerce.Services;
using Spring2025_Samples.Models;

namespace Maui.eCommerce.ViewModels
{
    public class ShoppingCartManagementViewModel : INotifyPropertyChanged
    {
        public int sender = 0;
        public string? Query { get; set; }


        private ProductServiceProxy _invsvc = ProductServiceProxy.Current;
        private ShoppingCartService _cartSvc = ShoppingCartService.Current;
        public Item? SelectedItem { get; set; }
        public Item? SelectedCartItem { get; set; }

        public ObservableCollection<Item?> Inventory
        {

            get
            {
                IEnumerable<Item?> filteredList = new ObservableCollection<Item?>(_invsvc.Products.Where(i => i?.Quantity > 0));
                if (sender == 1)
                {
                    filteredList = filteredList.Where(p =>p?.Product?.Name?.ToLower().Contains(Query?.ToLower() ?? string.Empty) ?? false);
                }
                if (sender == 2)
                {
                    filteredList = filteredList.Where(p =>p?.Product?.cost?.ToString()?.Contains(Query ?? string.Empty) ?? false);
                }
                sender = 0;
                return new ObservableCollection<Item?>(filteredList);
            }
        }

        public ObservableCollection<Item?> ShoppingCart
        {
            get
            {
                IEnumerable<Item?> filteredList = new ObservableCollection<Item?>(_cartSvc.CartItems.Where(i => i?.Quantity > 0));
                if (sender == 1)
                {
                    filteredList = filteredList.Where(p => p?.Product?.Name?.ToLower().Contains(Query?.ToLower() ?? string.Empty) ?? false);
                }
                if (sender == 2)
                {
                    filteredList = filteredList.Where(p => p?.Product?.cost?.ToString()?.Contains(Query ?? string.Empty) ?? false);
                }
                sender = 0;
                return new ObservableCollection<Item?>(filteredList);

            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (propertyName is null)
            {
                throw new ArgumentNullException(nameof(propertyName));
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
 
        public void ReturnItem()
        {
            if(SelectedCartItem != null)
            {
                var shouldRefresh = SelectedCartItem.Quantity >= 1;
                var updatedItem = _cartSvc.ReturnItem(SelectedCartItem);
                if (updatedItem != null && shouldRefresh)
                {
                    NotifyPropertyChanged(nameof(Inventory));
                    NotifyPropertyChanged(nameof(ShoppingCart));
                }
            }
        }

        public void RefreshProductList1()
        {
            sender = 1;
            NotifyPropertyChanged(nameof(Inventory));
        }
        public void RefreshProductList2()
        {
            sender = 2;
            NotifyPropertyChanged(nameof(Inventory));
        }
        public void ShopRefreshProductList1()
        {
            sender = 1;
            NotifyPropertyChanged(nameof(ShoppingCart));
        }
        public void ShopRefreshProductList2()
        {
            sender = 2;
            NotifyPropertyChanged(nameof(ShoppingCart));
        }
        public void RefreshProductList()
        {
            NotifyPropertyChanged(nameof(Inventory));
        }

       public void PurchaseItem()
        {
            if (SelectedItem != null && SelectedItem.ItemQuantity.HasValue)
            {
                var count = SelectedItem.ItemQuantity.Value;
                while (count > 0 && SelectedItem.Quantity > 0)
                {
                    var updatedItem = _cartSvc.AddOrUpdate(SelectedItem);
                    if (updatedItem != null)
                    {
                        count--;
                    }
                }
                NotifyPropertyChanged(nameof(Inventory));
                NotifyPropertyChanged(nameof(ShoppingCart));
            }
        }
        public void PurchaseOneItem()
        {
            if (SelectedItem != null && SelectedItem.ItemQuantity.HasValue)
            {

                    var updatedItem = _cartSvc.AddOrUpdate(SelectedItem);

                
                NotifyPropertyChanged(nameof(Inventory));
                NotifyPropertyChanged(nameof(ShoppingCart));
            }
        }
        
        public void PurchaseItem(Item item)
        {
        }


        //more or less copied from ProductViewModel
        private int? myNum;
        public int? ItemQuantity
        {
            get
            {
                return myNum;            
            }
            set
            {
                if (Model != null && myNum != value)
                {
                    myNum = value;
                    NotifyPropertyChanged();

                }
            }
        }
        public Item? Model { get; set; }
        private Item? cachedModel { get; set; }
        public ShoppingCartManagementViewModel()
        {
            Model = new Item();
        }
        public ShoppingCartManagementViewModel(Item? model)
        {
            Model = model;
            if (model != null)
            {
                cachedModel = new Item(model);
            }
        }
    }




}
