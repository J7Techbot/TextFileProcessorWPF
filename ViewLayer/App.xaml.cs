using DomainLayer.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using System.Windows;
using ViewLayer.Interfaces;
using ViewLayer.Services;
using ViewLayer.ViewModels;
using DomainLayer.BusinessLogic;
using DomainLayer.Interfaces;

namespace TextProcessor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider serviceProvider;

        public App()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            this.serviceProvider = services.BuildServiceProvider();
        }
        private void ConfigureServices(ServiceCollection services)
        {
            //Services
            services.AddSingleton<INavigationService, NavigationService>();            

            //Views
            services.AddSingleton<MainWindow>();

            //ViewModels
            services.AddSingleton<MainViewModel>();

            //Models
            services.AddSingleton<FileModel>();
            services.AddSingleton<IFileProcessor,FileProcessor>();
        }
        private void OnStartup(object sender, StartupEventArgs e)
        {
            serviceProvider.GetService<INavigationService>().ShowDialogInjectAsync<MainWindow>();

            SetupExceptionHandling();
        }

        private void SetupExceptionHandling()
        {
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
                ShowUnhandledException((Exception)e.ExceptionObject, "AppDomain.CurrentDomain.UnhandledException");

            DispatcherUnhandledException += (s, e) =>
            {
                ShowUnhandledException(e.Exception, "Application.Current.DispatcherUnhandledException");
                e.Handled = true;
            };

            TaskScheduler.UnobservedTaskException += (s, e) =>
            {
                ShowUnhandledException(e.Exception, "TaskScheduler.UnobservedTaskException");
                e.SetObserved();
            };
        }

        private void ShowUnhandledException(Exception exception, string source)
        {
            MessageBox.Show(exception.ToString());
        }
    }
}
