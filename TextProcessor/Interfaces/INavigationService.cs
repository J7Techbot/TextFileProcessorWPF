
using System.Windows;
using ViewLayer.ViewModels;

namespace ViewLayer.Interfaces
{
    public interface INavigationService
    {
        public void ShowDialog<T>(BaseViewModel vm) where T : Window, new();

        public void ShowDialogInjectAsync<T>(object param = null) where T : Window;
    }
}
