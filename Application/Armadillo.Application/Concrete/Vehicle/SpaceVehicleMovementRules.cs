using Armadillo.Core.Enumeration;
using Armadillo.Core.Navigation;
using Armadillo.Core.Vehicle;

namespace Armadillo.Application.Concrete.Vehicle
{
    public class SpaceVehicleMovementRules
    {
        Move move = new Move();
        Left left = new Left();
        Right right = new Right();

        public bool IsAvailable(ISpaceVehicle spaceVehicle, Navigator navigator, Movement movement, Position position)
        {
            bool available = true;

            if (!move.Check(spaceVehicle, navigator, movement, position))
            {
                available = false;
            }
            else if (!right.TurnRight(spaceVehicle, navigator))
            {
                available = false;
            }
            else if (!left.TurnLeft(spaceVehicle, navigator))
            {
                available = false;
            }

            return available;
        }
    }

    public class Move
    {
        public bool Check(ISpaceVehicle spaceVehicle, Navigator navigator, Movement movement, Position position)
        {
            var calculatedPosition = navigator.CalculatePositionAsync(position, movement).Result;

            if (calculatedPosition.X < 0 || calculatedPosition.X > spaceVehicle.DiscoveryArea.Width || calculatedPosition.Y < 0 || calculatedPosition.Y > spaceVehicle.DiscoveryArea.Height)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    public class Left
    {
        public bool TurnLeft(ISpaceVehicle spaceVehicle, Navigator navigator)
        {
            return true;
        }
    }

    public class Right
    {
        public bool TurnRight(ISpaceVehicle spaceVehicle, Navigator navigator)
        {
            return true;
        }
    }
}
