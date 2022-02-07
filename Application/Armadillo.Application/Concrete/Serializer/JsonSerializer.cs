using Armadillo.Core.Serializer;
using Newtonsoft.Json;

namespace Armadillo.Application.Concrete.Serializer
{
    public class JsonSerializer : IJsonSerializer
    {
        public string Serialize(object model)
        {
            return JsonConvert.SerializeObject(model);
        }

        public TModel Deserialize<TModel>(string model) where TModel : class
        {
            return JsonConvert.DeserializeObject<TModel>(model);
        }
    }
}
