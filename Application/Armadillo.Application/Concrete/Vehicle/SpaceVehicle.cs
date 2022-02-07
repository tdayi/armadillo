using Armadillo.Core.Enumeration;
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
        private readonly Guid areaId;
        private readonly string name;
        private readonly Position position;
        private readonly IEnumerable<INavigator> navigators;

        public SpaceVehicle(
            Guid areaId,
            string name,
            Position position,
            IEnumerable<INavigator> navigators)
        {
            this.areaId = areaId;
            this.name = name;
            this.position = position;
            this.navigators = navigators;
        }

        public string Name => name;

        public Guid AreaId => areaId;

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
                throw new ArgumentNullException(nameof(INavigator));
            }

            await navigator.SetPositionAsync(position, movement);
        }
    }
}
