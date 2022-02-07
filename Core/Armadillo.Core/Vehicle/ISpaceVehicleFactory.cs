using Armadillo.Core.Discovery;
using Armadillo.Core.Navigation;
using System.Threading.Tasks;

namespace Armadillo.Core.Vehicle
{
    public interface ISpaceVehicleFactory
    {
        Task<ISpaceVehicle> CreateAsync(IDiscoveryArea discoveryArea, Position position);
    }
}
