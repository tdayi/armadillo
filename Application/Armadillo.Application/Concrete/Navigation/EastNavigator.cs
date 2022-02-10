using Armadillo.Core.Enumeration;
using Armadillo.Core.Navigation;
using System.Threading.Tasks;

namespace Armadillo.Application.Navigation
{
    public class EastNavigator : Navigator
    {
        private readonly IPositionTracker positionTracker;

        public EastNavigator(IPositionTracker positionTracker) : base(positionTracker)
        {
            this.positionTracker = positionTracker;
        }

        public override Direction NavDirection => Direction.East;

        public async override Task SetNewPositionAsync(Position position, Movement movement, bool forced)
        {
            switch (movement)
            {
                case Movement.Right:
                    position.ChangeDirection(Direction.South);
                    break;
                case Movement.Left:
                    position.ChangeDirection(Direction.North);
                    break;
                case Movement.Move:
                    position.XIncrement();
                    break;
            }

            if (forced)
            {
                await positionTracker.SavePositionAsync(position);
            }
        }
    }
}
