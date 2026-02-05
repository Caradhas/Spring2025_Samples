using System.ComponentModel;
using System.Runtime.CompilerServices;
using Library.eCommerce.Services;

namespace Maui.eCommerce.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public string Display
        {
            get
            {
                return ShoppingCartService.Current.Total.Display;
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (propertyName is null)
            {
                throw new ArgumentNullException(nameof(Display));
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Display)));
        }



    }
}
