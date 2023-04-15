using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using ViewLayer.Interfaces;
using ViewLayer.ViewModels;

namespace ViewLayer.Services
{
    public class NavigationService : INavigationService
    {
        IServiceProvider _serviceProvider;

        public NavigationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Opens new window as dialog.
        /// </summary>
        /// <typeparam name="T">Type of window to open.</typeparam>
        /// <param name="vm">DataContext of window</param>        
        public void ShowDialog<T>(BaseViewModel vm) where T : Window, new()
        {
            var dialog = CreateNewWindowDialog<T>();
            dialog.DataContext = vm;
            dialog.ShowDialog();
        }

        /// <summary>
        /// Opens new window as dialog with injected dependencies.If window implements <see cref="IActivable"/> runs initialization before window is loaded.
        /// </summary>
        /// <typeparam name="T">Type of window to open.</typeparam>
        /// <param name="param"></param>
        public async void ShowDialogInjectAsync<T>(object param = null) where T : Window
        {
            var window = _serviceProvider.GetRequiredService<T>();

            if (window is IActivable activableWindow)
            {
                await activableWindow.ActivateAsync(param);
            }

            window.ShowDialog();
        }

        /// <summary>
        /// Create new window of type <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private Window CreateNewWindowDialog<T>() where T : Window, new()
        {
            T newWindow = new T();

            return newWindow;
        }
    }
}
