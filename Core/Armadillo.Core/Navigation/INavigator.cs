using Armadillo.Core.Enumeration;
using System.Threading.Tasks;

namespace Armadillo.Core.Navigation
{
    public interface INavigator
    {
        Direction Direction { get; }
        Task SetPositionAsync(Position position, Movement movement);
    }
}
