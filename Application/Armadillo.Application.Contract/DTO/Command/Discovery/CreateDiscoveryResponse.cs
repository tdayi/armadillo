using System;

namespace Armadillo.Application.Contract.DTO.Command.Discovery
{
    public class CreateDiscoveryResponse
    {
        public Guid DiscoveryId { get; set; }
        public string VehicleName { get; set; }
    }
}
