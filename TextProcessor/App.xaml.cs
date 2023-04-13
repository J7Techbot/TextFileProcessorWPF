using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using ViewLayer.Interfaces;
using ViewLayer.Services;
using ViewLayer.ViewModels;

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
            services.AddSingleton<DomainLayer.BusinessLogic.TextProcessor>();

            //Windows
            services.AddSingleton<MainWindow>();

            //ViewModels
            services.AddSingleton<MainViewModel>();

        }
        private void OnStartup(object sender, StartupEventArgs e)
        {
            serviceProvider.GetService<INavigationService>().ShowDialogInjectAsync<MainWindow>();
        }
    }
}
