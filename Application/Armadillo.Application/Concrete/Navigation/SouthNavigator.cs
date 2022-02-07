﻿using Armadillo.Core.Enumeration;
using Armadillo.Core.Navigation;
using System.Threading.Tasks;

namespace Armadillo.Application.Navigation
{
    public class SouthNavigator : INavigator
    {
        public Direction Direction => Direction.South;

        public async Task SetPositionAsync(Position position, Movement movement)
        {
            await Task.Run(() =>
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
            });
        }
    }
}