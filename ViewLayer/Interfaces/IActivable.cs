using System.Threading.Tasks;

namespace ViewLayer.Interfaces
{
    public interface IActivable
    {
        /// <summary>
        /// Initialize window before is fully loaded.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task ActivateAsync(object param);
    }
}