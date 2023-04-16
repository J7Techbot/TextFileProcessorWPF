using DomainLayer.Interfaces;
using System;
using System.Windows;

namespace ViewLayer.Services
{
    /// <summary>
    /// Is used for obtaining localization from a resource dictionary.Not fully implemented.
    /// </summary>
    public class LocalizationService : ILocalizationService
    {
        private readonly ResourceDictionary _resources;

        public LocalizationService()
        {
            _resources = new ResourceDictionary();
            _resources.Source = new Uri("pack://application:,,,/Resource/Localization/EN.xaml");
        }

        /// <summary>
        /// Find record in resource dictionary.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetValue(string key)
        {
            var value = (string)_resources[key];
            return value;
        }
    }
}
