using Armadillo.Application.Contract.DTO.Command.Navigate;
using Armadillo.Core.Cache;
using Armadillo.Core.Exception;
using Armadillo.Core.Vehicle;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Armadillo.Application.Handler.Command.Navigate
{
    public class NavigateHandler : IRequestHandler<DoNavigateRequest, DoNavigateResponse>
    {
        private readonly ICacheManager cacheManager;

        public NavigateHandler(
            ICacheManager cacheManager)
        {
            this.cacheManager = cacheManager;
        }

        public async Task<DoNavigateResponse> Handle(DoNavigateRequest request, CancellationToken cancellationToken)
        {
            var vehicle = cacheManager.GetByKey<ISpaceVehicle>(request.VehicleName);
            if (vehicle is null)
            {
                throw new BusinessException($"{request.VehicleName} is not found!");
            }

            await vehicle.NavigateAsync(request.Movement);

            var position = await vehicle.GetPositionAsync();

            return new DoNavigateResponse
            {
                X = position.X,
                Y = position.Y,
                Direction = position.Direction
            };
        }
    }
}
