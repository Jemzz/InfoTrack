using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Scrapper.Core.Utilities
{
    public static class Extensions
    {
        private static JsonSerializerSettings GetSettings()
        {
            return new JsonSerializerSettings
            {
                Formatting = Formatting.None,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

        public static T FromJsonString<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json, GetSettings())!;
        }
    }
}
