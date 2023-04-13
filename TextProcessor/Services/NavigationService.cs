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

        private Window CreateNewWindowDialog<T>() where T : Window, new()
        {
            T newWindow = new T();

            return newWindow;
        }
        public void ShowDialog<T>(BaseViewModel vm) where T : Window, new()
        {
            var dialog = CreateNewWindowDialog<T>();
            dialog.DataContext = vm;
            dialog.ShowDialog();
        }

        public async void ShowDialogInjectAsync<T>(object param = null) where T : Window
        {
            var window = _serviceProvider.GetRequiredService<T>();

            if (window is IActivable activableWindow)
            {
                await activableWindow.ActivateAsync(param);
            }

            window.ShowDialog();
        }
    }
}
