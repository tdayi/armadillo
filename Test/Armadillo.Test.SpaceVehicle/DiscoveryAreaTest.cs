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
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Armadillo.Test.SpaceVehicle
{
    public class DiscoveryAreaTest
    {
        private readonly IJsonSerializer jsonSerializer;
        private readonly IPositionTracker positionTracker;
        private readonly Mock<IAppSettings> appSettings = new Mock<IAppSettings>();
        private readonly Mock<ILogger<PositionTracker>> positionTrackerLogger = new Mock<ILogger<PositionTracker>>();
        private readonly Mock<ILogger<SpaceVehicleFactory>> spaceVehicleFactoryLogger = new Mock<ILogger<SpaceVehicleFactory>>();
        private readonly Mock<ICacheManager> cacheManager = new Mock<ICacheManager>();
        private readonly IDiscoveryAreaFactory discoveryAreaFactory;
        private readonly ISpaceVehicleFactory spaceVehicleFactory;

        public DiscoveryAreaTest()
        {
            jsonSerializer = new JsonSerializer();

            appSettings.Setup(x => x.Tracker).Returns(new Tracker
            {
                Path = "armadillo_position/"
            });

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
        public async Task Only_Move_Should_Return_Discovery_Area_Fail_Async()
        {
            int w = 5;
            int h = 5;

            //5 5
            //1 2 N
            //MM MM

            var area = await discoveryAreaFactory.CreateAreaAsync(w, h);

            var vehicle = await spaceVehicleFactory.CreateAsync(area, new Position(1, 2, Direction.North));

            await vehicle.NavigateAsync(Movement.Move);
            await vehicle.NavigateAsync(Movement.Move);
            await vehicle.NavigateAsync(Movement.Move);

            Func<Task> action = () => vehicle.NavigateAsync(Movement.Move);

            var ex = await Record.ExceptionAsync(action);

            Assert.True(ex.Message == $"Discovery area width: {w} and height: {h} is not avaliable next movement!");
        }

        [Fact]
        public async Task Zero_Discovery_Area_Move_Should_Return_Discovery_Area_Fail_Async()
        {
            int w = 0;
            int h = 0;

            //0 0
            //0 0 N
            //M

            var area = await discoveryAreaFactory.CreateAreaAsync(w, h);

            var vehicle = await spaceVehicleFactory.CreateAsync(area, new Position(0, 0, Direction.North));

            Func<Task> action = () => vehicle.NavigateAsync(Movement.Move);

            var ex = await Record.ExceptionAsync(action);

            Assert.True(ex.Message == $"Discovery area width: {w} and height: {h} is not avaliable next movement!");
        }

        [Fact]
        public async Task Zero_Discovery_Area_Left_Move_Should_Return_Discovery_Area_Fail_Async()
        {
            int w = 0;
            int h = 0;

            //0 0
            //0 0 N
            //LM

            var area = await discoveryAreaFactory.CreateAreaAsync(w, h);

            var vehicle = await spaceVehicleFactory.CreateAsync(area, new Position(0, 0, Direction.North));

            await vehicle.NavigateAsync(Movement.Left);

            Func<Task> action = () => vehicle.NavigateAsync(Movement.Move);

            var ex = await Record.ExceptionAsync(action);

            Assert.True(ex.Message == $"Discovery area width: {w} and height: {h} is not avaliable next movement!");
        }
    }
}
