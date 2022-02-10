using Armadillo.Core.Enumeration;
using System.Threading.Tasks;

namespace Armadillo.Core.Navigation
{
    public interface INavigator
    {
        Direction Direction { get; }
        Task ChangePositionAsync(Position position, Movement movement);
        Task<Position> CalculatedNextPositionAsync(Position position, Movement movement);
    }
}
