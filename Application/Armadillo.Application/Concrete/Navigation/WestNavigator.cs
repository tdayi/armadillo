using Armadillo.Core.Enumeration;
using Armadillo.Core.Navigation;
using System.Threading.Tasks;

namespace Armadillo.Application.Navigation
{
    public class WestNavigator : Navigator
    {
        private readonly IPositionTracker positionTracker;

        public WestNavigator(IPositionTracker positionTracker) : base(positionTracker)
        {
            this.positionTracker = positionTracker;
        }

        public override Direction Direction => Direction.West;

        public override async Task ChangePositionAsync(Position position, Movement movement, bool forced)
        {
            switch (movement)
            {
                case Movement.Right:
                    position.ChangeDirection(Direction.North);
                    break;
                case Movement.Left:
                    position.ChangeDirection(Direction.South);
                    break;
                case Movement.Move:
                    position.XDecrease();
                    break;
            }

            if (forced)
            {
                await positionTracker.SavePositionAsync(position);
            }
        }
    }
}
