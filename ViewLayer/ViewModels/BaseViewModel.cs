using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ViewLayer.ViewModels
{
    /// <summary>
    /// Implements INotifyPropertyCHanged
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }    
}
