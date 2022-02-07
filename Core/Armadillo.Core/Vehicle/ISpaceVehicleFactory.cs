using Armadillo.Core.Navigation;
using System;
using System.Threading.Tasks;

namespace Armadillo.Core.Vehicle
{
    public interface ISpaceVehicleFactory
    {
        Task<ISpaceVehicle> CreateAsync(Guid areaId, Position position);
    }
}
