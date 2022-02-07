using Armadillo.Application.Contract.DTO.Command.Discovery;
using Armadillo.Core.Cache;
using Armadillo.Core.Discovery;
using Armadillo.Core.Enumeration;
using Armadillo.Core.Navigation;
using Armadillo.Core.Vehicle;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Armadillo.Application.Handler.Command.Discovery
{
    public class DiscoveryHandler : IRequestHandler<CreateDiscoveryRequest, CreateDiscoveryResponse>
    {
        private readonly ICacheManager cacheManager;
        private readonly IDiscoveryAreaFactory discoveryAreaFactory;
        private readonly ISpaceVehicleFactory spaceVehicleFactory;

        public DiscoveryHandler(
            ICacheManager cacheManager,
            IDiscoveryAreaFactory discoveryAreaFactory,
            ISpaceVehicleFactory spaceVehicleFactory)
        {
            this.cacheManager = cacheManager;
            this.discoveryAreaFactory = discoveryAreaFactory;
            this.spaceVehicleFactory = spaceVehicleFactory;
        }

        public async Task<CreateDiscoveryResponse> Handle(CreateDiscoveryRequest request, CancellationToken cancellationToken)
        {
            var area = await discoveryAreaFactory.CreateAreaAsync(request.Width, request.Height);

            var vehicle = await spaceVehicleFactory.CreateAsync(area.Id, new Position(0, 0, Direction.North));

            cacheManager.Set<IDiscoveryArea>(area.Id.ToString(), area);
            cacheManager.Set<ISpaceVehicle>(vehicle.Name, vehicle);

            return new CreateDiscoveryResponse
            {
                Id = area.Id
            };
        }
    }
}
