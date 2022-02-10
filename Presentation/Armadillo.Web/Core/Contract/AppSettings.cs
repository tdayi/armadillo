using Armadillo.Core.Contract;

namespace Armadillo.Web.Core.Contract
{
    public class AppSettings : IAppSettings
    {
        public Tracker Tracker { get; protected set; }
    }
}
