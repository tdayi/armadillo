using System.Threading.Tasks;

namespace Armadillo.Core.Discovery
{
    public interface IDiscoveryAreaFactory
    {
        Task<IDiscoveryArea> CreateAreaAsync(int width, int height);
    }
}
