using Armadillo.Core.Contract;
using Armadillo.Core.Navigation;
using Armadillo.Core.Serializer;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Armadillo.Application.Concrete.Navigation
{
    public class PositionTracker : IPositionTracker
    {
        private readonly ILogger<PositionTracker> logger;
        private readonly IAppSettings appSettings;
        private readonly IJsonSerializer jsonSerializer;

        public PositionTracker(
            ILogger<PositionTracker> logger,
            IAppSettings appSettings,
            IJsonSerializer jsonSerializer)
        {
            this.logger = logger;
            this.appSettings = appSettings;
            this.jsonSerializer = jsonSerializer;
        }

        public async Task SavePositionAsync(Position position)
        {
            //TODO can be better.

            try
            {
                if (!Directory.Exists(appSettings.Tracker.Path))
                {
                    Directory.CreateDirectory(appSettings.Tracker.Path);
                }

                await File.AppendAllTextAsync(
                    Path.Combine(
                        appSettings.Tracker.Path,
                        $"{DateTime.Now.ToString("yyyy-MM-dd")}-position.txt"),
                        $"{jsonSerializer.Serialize(position)}{Environment.NewLine}");
            }
            catch (Exception ex)
            {
                logger.LogError("save position error!", ex);
            }
        }
    }
}
