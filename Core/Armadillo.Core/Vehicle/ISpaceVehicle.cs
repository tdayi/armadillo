using Armadillo.Core.Discovery;
using Armadillo.Core.Enumeration;
using Armadillo.Core.Navigation;
using System;
using System.Threading.Tasks;

namespace Armadillo.Core.Vehicle
{
    public interface ISpaceVehicle
    {
        string Name { get; }
        IDiscoveryArea DiscoveryArea { get; }
        Task<Position> GetPositionAsync();
        Task NavigateAsync(Movement movement);
    }
}
