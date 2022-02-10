using Armadillo.Core.Enumeration;
using System.Threading.Tasks;

namespace Armadillo.Core.Navigation
{
    public abstract class Navigator : INavigator
    {
        private readonly IPositionTracker positionTracker;

        public Direction Direction => NavDirection;

        public abstract Direction NavDirection { get; }

        protected Navigator(
            IPositionTracker positionTracker)
        {
            this.positionTracker = positionTracker;
        }

        public async Task ChangePositionAsync(Position position, Movement movement)
        {
            await SetNewPositionAsync(position, movement, true);
        }

        public async Task<Position> CalculatedNextPositionAsync(Position position, Movement movement)
        {
            Position clonePosition = (Position)position.Clone();
            await SetNewPositionAsync(clonePosition, movement, false);

            return clonePosition;
        }

        public abstract Task SetNewPositionAsync(Position position, Movement movement, bool forced);
    }
}