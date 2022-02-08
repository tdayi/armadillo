using Armadillo.Application.Discovery;
using Armadillo.Application.Navigation;
using Armadillo.Application.Vehicle;
using Armadillo.Core.Cache;
using Armadillo.Core.Discovery;
using Armadillo.Core.Enumeration;
using Armadillo.Core.Navigation;
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
        private readonly Mock<ILogger<SpaceVehicleFactory>> logger = new Mock<ILogger<SpaceVehicleFactory>>();
        private readonly Mock<ICacheManager> cacheManager = new Mock<ICacheManager>();
        private readonly IDiscoveryAreaFactory discoveryAreaFactory;
        private readonly ISpaceVehicleFactory spaceVehicleFactory;

        public DiscoveryAreaTest()
        {
            var navigators = new List<INavigator>
            {
                new EastNavigator(),
                new NorthNavigator(),
                new SouthNavigator(),
                new WestNavigator()
            };

            this.discoveryAreaFactory = new DiscoveryAreaFactory();
            this.spaceVehicleFactory = new SpaceVehicleFactory(cacheManager.Object, logger.Object, navigators);
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
