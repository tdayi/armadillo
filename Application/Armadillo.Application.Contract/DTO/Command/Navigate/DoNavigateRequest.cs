using Armadillo.Core.Enumeration;
using MediatR;
using System;

namespace Armadillo.Application.Contract.DTO.Command.Navigate
{
    public class DoNavigateRequest : IRequest<DoNavigateResponse>
    {
        public Guid DiscoveryId { get; set; }
        public Movement Movement { get; set; }
        public string VehicleName { get; set; }
    }
}
