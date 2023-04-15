
using System.Windows;
using ViewLayer.ViewModels;

namespace ViewLayer.Interfaces
{
    public interface INavigationService
    {
        /// <summary>
        /// Opens new window as dialog.
        /// </summary>
        /// <typeparam name="T">Type of window to open.</typeparam>
        /// <param name="vm">DataContext of window</param>
        public void ShowDialog<T>(BaseViewModel vm) where T : Window, new();

        /// <summary>
        /// Opens new window as dialog with injected dependencies.If window implements <see cref="IActivable"/> runs initialization before window is loaded.
        /// </summary>
        /// <typeparam name="T">Type of window to open.</typeparam>
        /// <param name="param"></param>
        public void ShowDialogInjectAsync<T>(object param = null) where T : Window;
    }
}
