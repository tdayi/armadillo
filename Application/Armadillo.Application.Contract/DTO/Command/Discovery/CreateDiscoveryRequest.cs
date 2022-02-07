using Armadillo.Core.Enumeration;
using MediatR;

namespace Armadillo.Application.Contract.DTO.Command.Discovery
{
    public class CreateDiscoveryRequest : IRequest<CreateDiscoveryResponse>
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int? X { get; set; }
        public int? Y { get; set; }
        public Direction? Direction { get; set; }
    }
}
