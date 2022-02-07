using MediatR;

namespace Armadillo.Application.Contract.DTO.Command.Discovery
{
    public class CreateDiscoveryRequest : IRequest<CreateDiscoveryResponse>
    {
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
