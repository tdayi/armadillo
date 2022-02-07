using Armadillo.Core.Enumeration;
using Armadillo.Core.Navigation;
using System.Threading.Tasks;

namespace Armadillo.Application.Navigation
{
    public class NorthNavigator : INavigator
    {
        public Direction Direction => Direction.North;

        public async Task<Position> CalculatePositionAsync(Position position, Movement movement)
        {
            return await Task.Run(() =>
            {
                if (movement == Movement.Move)
                {
                    Position newPosition = (Position)position.Clone();
                    newPosition.YIncrement();

                    return newPosition;
                }
                else
                {
                    return position;
                }
            });
        }

        public async Task SetPositionAsync(Position position, Movement movement)
        {
            await Task.Run(() =>
            {
                switch (movement)
                {
                    case Movement.Right:
                        position.ChangeDirection(Direction.East);
                        break;
                    case Movement.Left:
                        position.ChangeDirection(Direction.West);
                        break;
                    case Movement.Move:
                        position.YIncrement();
                        break;
                }
            });
        }
    }
}
