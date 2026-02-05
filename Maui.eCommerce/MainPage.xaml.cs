using Library.eCommerce.Services;
using Maui.eCommerce.ViewModels;

namespace Maui.eCommerce
{
    public partial class MainPage : ContentPage
    {
        public MainViewModel ViewModel { get; set; }

        public MainPage()
        {
            InitializeComponent();
            ViewModel = new MainViewModel();
            BindingContext = ViewModel;
        }

        private void InventoryClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//InventoryManagement");
        }

        private void ShoppingCartCLicked(object sender, EventArgs e)
        {
            ViewModel.NotifyPropertyChanged();
            Shell.Current.GoToAsync("//ShoppingCartManagement");
        }


    }
}
