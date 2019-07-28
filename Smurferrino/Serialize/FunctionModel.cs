using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Smurferrino.Models;

namespace Smurferrino.Serialize
{
    public class FunctionModel
    {
        [JsonProperty]
        public BaseFunctionModel Model { get; set; }

        [JsonIgnore]
        public string Json { get; set; }

        public FunctionModel(BaseFunctionModel model)
        {
            Model = model;
        }
    }
}
