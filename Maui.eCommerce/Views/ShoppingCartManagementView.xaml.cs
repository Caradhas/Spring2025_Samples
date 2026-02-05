using Maui.eCommerce.ViewModels;
using Library.eCommerce.Models;

namespace Maui.eCommerce.Views;

public partial class ShoppingCartManagementView : ContentPage
{
	public ShoppingCartManagementView()
	{
		InitializeComponent();
		BindingContext = new ShoppingCartManagementViewModel();
	}
    private void AddToCartClicked(object sender, EventArgs e)
    {
        (BindingContext as ShoppingCartManagementViewModel)?.PurchaseItem();
    }
    private void RemoveFromCartClicked(object sender, EventArgs e)
    {
        (BindingContext as ShoppingCartManagementViewModel)?.ReturnItem();
    }
    private void GoBackClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }
    private void SearchClicked(object sender, EventArgs e)
    {
        (BindingContext as ShoppingCartManagementViewModel)?.RefreshProductList1();
    }
    private void PriceSearchClicked(object sender, EventArgs e)
    {
        (BindingContext as ShoppingCartManagementViewModel)?.RefreshProductList2();
    }
    private void ShopSearchClicked(object sender, EventArgs e)
    {
        (BindingContext as ShoppingCartManagementViewModel)?.ShopRefreshProductList1();
    }
    private void ShopPriceSearchClicked(object sender, EventArgs e)
    {
        (BindingContext as ShoppingCartManagementViewModel)?.ShopRefreshProductList2();
    }
    private void AddOneToCartClicked(object sender, EventArgs e)
    {
        (BindingContext as ShoppingCartManagementViewModel)?.PurchaseOneItem();
    }




}