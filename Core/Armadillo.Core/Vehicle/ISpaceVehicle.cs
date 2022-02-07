using Armadillo.Core.Enumeration;
using Armadillo.Core.Navigation;
using System;
using System.Threading.Tasks;

namespace Armadillo.Core.Vehicle
{
    public interface ISpaceVehicle
    {
        Guid AreaId { get; }
        string Name { get; }
        Task<Position> GetPositionAsync();
        Task NavigateAsync(Movement movement);
    }
}
