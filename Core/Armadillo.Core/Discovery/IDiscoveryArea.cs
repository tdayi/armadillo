using System;

namespace Armadillo.Core.Discovery
{
    public interface IDiscoveryArea
    {
        Guid Id { get; }
        int Width { get; }
        int Height { get; }
    }
}
