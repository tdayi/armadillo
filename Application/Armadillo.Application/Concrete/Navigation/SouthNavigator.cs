using Armadillo.Core.Enumeration;
using Armadillo.Core.Navigation;
using System.Threading.Tasks;

namespace Armadillo.Application.Navigation
{
    public class SouthNavigator : Navigator
    {
        private readonly IPositionTracker positionTracker;

        public SouthNavigator(IPositionTracker positionTracker) : base(positionTracker)
        {
            this.positionTracker = positionTracker;
        }

        public override Direction NavDirection => Direction.South;

        public override async Task ChangePositionAsync(Position position, Movement movement, bool forced)
        {
            switch (movement)
            {
                case Movement.Right:
                    position.ChangeDirection(Direction.West);
                    break;
                case Movement.Left:
                    position.ChangeDirection(Direction.East);
                    break;
                case Movement.Move:
                    position.YDecrease();
                    break;
            }

            if (forced)
            {
                await positionTracker.SavePositionAsync(position);
            }
        }
    }
}
