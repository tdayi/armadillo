using Armadillo.Core.Enumeration;

namespace Armadillo.Application.Contract.DTO.Query.Position
{
    public class GetPositionResponse
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Direction Direction { get; set; }
    }
}
