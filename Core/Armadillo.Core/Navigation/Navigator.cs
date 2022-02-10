using Armadillo.Core.Enumeration;
using System.Threading.Tasks;

namespace Armadillo.Core.Navigation
{
    public abstract class Navigator
    {
        private readonly IPositionTracker positionTracker;

        protected Navigator(
            IPositionTracker positionTracker)
        {
            this.positionTracker = positionTracker;
        }

        public abstract Direction Direction { get; }

        public async Task SetPosition(Position position, Movement movement)
        {
            await ChangePositionAsync(position, movement, true);
        }

        public async Task<Position> CalculatePositionAsync(Position position, Movement movement)
        {
            Position clonePosition = (Position)position.Clone();
            await ChangePositionAsync(clonePosition, movement, false);

            return clonePosition;
        }

        public abstract Task ChangePositionAsync(Position position, Movement movement, bool forced);
    }
}
