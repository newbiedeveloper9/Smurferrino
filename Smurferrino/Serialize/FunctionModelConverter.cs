using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Smurferrino.Models;

namespace Smurferrino.Serialize
{
    public class FunctionModelConverter : JsonConverter
    {
        public override bool CanWrite => false;
        public override bool CanRead => true;

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(IModel);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            /*var jsonObject = JObject.Load(reader);
            var functionModel = default(IModel);
            switch (jsonObject.GetValue("FunctionName").ToString())
            {
                case "Bunny":
                    functionModel = new BunnyModel();
                    break;
                case "Trigger":
                    functionModel = new TriggerModel();
                    break;
            }

            serializer.Populate(jsonObject.CreateReader(), functionModel);
            return functionModel;*/
            throw new NotImplementedException();
        }
    }
}
