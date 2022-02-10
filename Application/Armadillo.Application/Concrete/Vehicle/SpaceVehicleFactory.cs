using Armadillo.Core.Cache;
using Armadillo.Core.Constant;
using Armadillo.Core.Discovery;
using Armadillo.Core.Navigation;
using Armadillo.Core.Vehicle;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Armadillo.Application.Vehicle
{
    public class SpaceVehicleFactory : ISpaceVehicleFactory
    {
        private readonly ICacheManager cacheManager;
        private readonly ILogger<SpaceVehicleFactory> logger;
        private readonly IEnumerable<Navigator> navigators;

        private readonly SemaphoreSlim spaceVehicleCreateLock;

        public SpaceVehicleFactory(
            ICacheManager cacheManager,
            ILogger<SpaceVehicleFactory> logger,
            IEnumerable<Navigator> navigators)
        {
            this.cacheManager = cacheManager;
            this.logger = logger;
            this.navigators = navigators;
            this.spaceVehicleCreateLock = new SemaphoreSlim(1, 1);
        }

        public async Task<ISpaceVehicle> CreateAsync(IDiscoveryArea discoveryArea, Position position)
        {
            return await Task.Run(async () =>
            {
                SpaceVehicle spaceVehicle = null;

                try
                {
                    await spaceVehicleCreateLock.WaitAsync();

                    int number = cacheManager.GetByKey<int?>(CacheKey.MaxArmadilloNumber) ?? 0;

                    number = number + 1;

                    cacheManager.Set<int>(CacheKey.MaxArmadilloNumber, number);

                    spaceVehicle = new SpaceVehicle($"Armadillo-{number}", position, discoveryArea, navigators);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex.Message, "we got error to create space vehicle!");
                }
                finally
                {
                    spaceVehicleCreateLock.Release();
                }

                return spaceVehicle;
            });
        }
    }
}
