using Armadillo.Core.Discovery;
using System;

namespace Armadillo.Application.Discovery
{
    public class DiscoveryArea : IDiscoveryArea
    {
        private readonly int width;
        private readonly int height;
        private readonly Guid id;

        public DiscoveryArea(
            int width,
            int height)
        {
            this.width = width;
            this.height = height;
            this.id = Guid.NewGuid();
        }

        public Guid Id => id;

        public int Width => width;

        public int Height => height;
    }
}
