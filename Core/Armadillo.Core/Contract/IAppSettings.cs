namespace Armadillo.Core.Contract
{
    public interface IAppSettings
    {
        public Tracker Tracker { get; }
    }

    public class Tracker
    {
        public string Path { get; set; }
    }
}
