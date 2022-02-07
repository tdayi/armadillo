namespace Armadillo.Core.Cache
{
    public interface ICacheManager
    {
        bool CacheContains(string key);
        T GetByKey<T>(string key);
        void Remove(string key);
        void Set<T>(string key, T data);
    }
}
