using Armadillo.Core.Enumeration;

namespace Armadillo.Core.Navigation
{
    public class Position
    {
        public int X { get; protected set; }
        public int Y { get; protected set; }
        public Direction Direction { get; protected set; }

        public Position(
            int x,
            int y,
            Direction direction)
        {
            X = x;
            Y = y;
            Direction = direction;
        }

        public void XIncrement()
        {
            X++;
        }

        public void XDecrease()
        {
            X--;
        }

        public void YIncrement()
        {
            Y++;
        }

        public void YDecrease()
        {
            Y--;
        }

        public void ChangeDirection(Direction direction)
        {
            Direction = direction;
        }
    }
}
