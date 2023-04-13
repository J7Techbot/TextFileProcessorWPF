using System.Threading.Tasks;

namespace ViewLayer.Interfaces
{
    public interface IActivable
    {
        Task ActivateAsync(object param);
    }
}