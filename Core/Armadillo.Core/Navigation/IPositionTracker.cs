using System.Threading.Tasks;

namespace Armadillo.Core.Navigation
{
    public interface IPositionTracker
    {
        Task SavePositionAsync(Position position);
    }
}
