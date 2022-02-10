namespace Armadillo.Core.Contract
{
    public interface IAppSettings
    {
        public Tracker Tracker { get; }
    }

    public class Tracker
    {
        public Tracker(string path)
        {
            Path = path;
        }

        public string Path { get; protected set; }
    }
}
