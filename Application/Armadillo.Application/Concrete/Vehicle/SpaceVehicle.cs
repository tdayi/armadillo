using Armadillo.Application.Concrete.Vehicle;
using Armadillo.Core.Discovery;
using Armadillo.Core.Enumeration;
using Armadillo.Core.Exception;
using Armadillo.Core.Navigation;
using Armadillo.Core.Vehicle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Armadillo.Application.Vehicle
{
    public class SpaceVehicle : ISpaceVehicle
    {
        private readonly string name;
        private readonly Position position;
        private readonly IDiscoveryArea discoveryArea;
        private readonly IEnumerable<INavigator> navigators;

        public SpaceVehicle(
            string name,
            Position position,
            IDiscoveryArea discoveryArea,
            IEnumerable<INavigator> navigators)
        {
            this.name = name;
            this.position = position;
            this.discoveryArea = discoveryArea;
            this.navigators = navigators;
        }

        public string Name => name;

        public IDiscoveryArea DiscoveryArea => discoveryArea;

        public async Task<Position> GetPositionAsync()
        {
            return await Task.Run(() =>
            {
                return position;
            });
        }

        public async Task NavigateAsync(Movement movement)
        {
            var navigator = navigators.Where(x => x.Direction == position.Direction).FirstOrDefault();
            if (navigator is null)
            {
                throw new ArgumentNullException(nameof(Navigator));
            }

            SpaceVehicleMovementRules spaceVehicleMovementRules = new SpaceVehicleMovementRules();
            if (!spaceVehicleMovementRules.IsAvailable(this, navigator, movement, position))
            {
                throw new BusinessException($"Discovery area width: {discoveryArea.Width} and height: {discoveryArea.Height} is not avaliable next movement!");
            }

            await navigator.ChangePositionAsync(position, movement);
        }
    }
}
