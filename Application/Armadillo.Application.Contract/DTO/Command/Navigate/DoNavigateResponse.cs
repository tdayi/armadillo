using Armadillo.Core.Enumeration;

namespace Armadillo.Application.Contract.DTO.Command.Navigate
{
    public class DoNavigateResponse
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Direction Direction { get; set; }
    }
}
