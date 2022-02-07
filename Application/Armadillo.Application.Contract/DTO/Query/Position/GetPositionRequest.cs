using MediatR;
using System;

namespace Armadillo.Application.Contract.DTO.Query.Position
{
    public class GetPositionRequest : IRequest<GetPositionResponse>
    {
        public Guid DiscoveryId { get; set; }
        public string VehicleName { get; set; }
    }
}
