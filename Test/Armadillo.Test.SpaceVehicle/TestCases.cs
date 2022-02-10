using Armadillo.Application.Concrete.Navigation;
using Armadillo.Application.Concrete.Serializer;
using Armadillo.Application.Discovery;
using Armadillo.Application.Navigation;
using Armadillo.Application.Vehicle;
using Armadillo.Core.Cache;
using Armadillo.Core.Contract;
using Armadillo.Core.Discovery;
using Armadillo.Core.Enumeration;
using Armadillo.Core.Navigation;
using Armadillo.Core.Serializer;
using Armadillo.Core.Vehicle;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Armadillo.Test.SpaceVehicle
{
    public class TestCases
    {
        private readonly IJsonSerializer jsonSerializer;
        private readonly IPositionTracker positionTracker;
        private readonly Mock<IAppSettings> appSettings = new Mock<IAppSettings>();
        private readonly Mock<ILogger<PositionTracker>> positionTrackerLogger = new Mock<ILogger<PositionTracker>>();
        private readonly Mock<ILogger<SpaceVehicleFactory>> spaceVehicleFactoryLogger = new Mock<ILogger<SpaceVehicleFactory>>();
        private readonly Mock<ICacheManager> cacheManager = new Mock<ICacheManager>();
        private readonly IDiscoveryAreaFactory discoveryAreaFactory;
        private readonly ISpaceVehicleFactory spaceVehicleFactory;

        public TestCases()
        {
            jsonSerializer = new JsonSerializer();

            appSettings.Setup(x => x.Tracker).Returns(new Tracker("armadillo_position/"));

            positionTracker = new PositionTracker(positionTrackerLogger.Object, appSettings.Object, jsonSerializer);

            var navigators = new List<Navigator>
            {
                new EastNavigator(positionTracker),
                new NorthNavigator(positionTracker),
                new SouthNavigator(positionTracker),
                new WestNavigator(positionTracker)
            };

            this.discoveryAreaFactory = new DiscoveryAreaFactory();
            this.spaceVehicleFactory = new SpaceVehicleFactory(cacheManager.Object, spaceVehicleFactoryLogger.Object, navigators);
        }

        [Fact]
        public async Task Test_Case1_Should_Return_13N_Async()
        {
            //5 5
            //1 2 N
            //LM LM LM LM M

            var area = await discoveryAreaFactory.CreateAreaAsync(5, 5);

            var vehicle = await spaceVehicleFactory.CreateAsync(area, new Position(1, 2, Direction.North));

            await vehicle.NavigateAsync(Movement.Left);
            await vehicle.NavigateAsync(Movement.Move);

            await vehicle.NavigateAsync(Movement.Left);
            await vehicle.NavigateAsync(Movement.Move);

            await vehicle.NavigateAsync(Movement.Left);
            await vehicle.NavigateAsync(Movement.Move);

            await vehicle.NavigateAsync(Movement.Left);
            await vehicle.NavigateAsync(Movement.Move);

            await vehicle.NavigateAsync(Movement.Move);

            var currentPosition = await vehicle.GetPositionAsync();

            Assert.True((currentPosition.X == 1 && currentPosition.Y == 3 && currentPosition.Direction == Direction.North));
        }

        [Fact]
        public async Task Test_Case2_Should_Return_51E_Async()
        {
            //5 5
            //3 3 E
            //MM RM MR MR RM

            var area = await discoveryAreaFactory.CreateAreaAsync(5, 5);

            var vehicle = await spaceVehicleFactory.CreateAsync(area, new Position(3, 3, Direction.East));

            await vehicle.NavigateAsync(Movement.Move);
            await vehicle.NavigateAsync(Movement.Move);

            await vehicle.NavigateAsync(Movement.Right);
            await vehicle.NavigateAsync(Movement.Move);

            await vehicle.NavigateAsync(Movement.Move);
            await vehicle.NavigateAsync(Movement.Right);

            await vehicle.NavigateAsync(Movement.Move);
            await vehicle.NavigateAsync(Movement.Right);

            await vehicle.NavigateAsync(Movement.Right);
            await vehicle.NavigateAsync(Movement.Move);

            var currentPosition = await vehicle.GetPositionAsync();

            Assert.True((currentPosition.X == 5 && currentPosition.Y == 1 && currentPosition.Direction == Direction.East));
        }
    }
}
