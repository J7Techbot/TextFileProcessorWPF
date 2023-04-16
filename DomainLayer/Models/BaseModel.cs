using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DomainLayer.Models
{
    /// <summary>
    /// Implements INotifyPropertyCHanged
    /// </summary>
    public class BaseModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
