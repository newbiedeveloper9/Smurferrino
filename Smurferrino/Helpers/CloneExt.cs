using Newtonsoft.Json;

namespace Smurferrino.Helpers
{
    public static class CloneExt
    {
        public static T Clone<T>(this T source)
        {
            var settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All
            };

            var serialized = JsonConvert.SerializeObject(source, settings);
            return JsonConvert.DeserializeObject<T>(serialized, settings);
        }
    }
}
