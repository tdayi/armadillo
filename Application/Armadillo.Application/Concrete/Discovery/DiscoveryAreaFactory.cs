using Armadillo.Core.Discovery;
using System.Threading.Tasks;

namespace Armadillo.Application.Discovery
{
    public class DiscoveryAreaFactory : IDiscoveryAreaFactory
    {
        public async Task<IDiscoveryArea> CreateAreaAsync(int width, int height)
        {
            return await Task.Run(() =>
            {
                return new DiscoveryArea(width, height);
            });
        }
    }
}
