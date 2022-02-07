using Armadillo.Application.Contract.DTO.Query.Position;
using Armadillo.Core.Cache;
using Armadillo.Core.Vehicle;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Armadillo.Application.Handler.Query.Position
{
    public class GetPositionHandler : IRequestHandler<GetPositionRequest, GetPositionResponse>
    {
        private readonly ICacheManager cacheManager;

        public GetPositionHandler(
            ICacheManager cacheManager)
        {
            this.cacheManager = cacheManager;
        }

        public async Task<GetPositionResponse> Handle(GetPositionRequest request, CancellationToken cancellationToken)
        {
            var vehicle = cacheManager.GetByKey<ISpaceVehicle>(request.VehicleName);

            var position = await vehicle.GetPositionAsync();

            return new GetPositionResponse
            {
                X = position.X,
                Y = position.Y,
                Direction = position.Direction
            };
        }
    }
}
