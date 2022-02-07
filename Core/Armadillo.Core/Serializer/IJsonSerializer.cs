namespace Armadillo.Core.Serializer
{
    public interface IJsonSerializer
    {
        TModel Deserialize<TModel>(string model) where TModel : class;
        string Serialize(object model);
    }
}
