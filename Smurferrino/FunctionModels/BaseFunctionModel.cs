using System.Threading.Tasks;
using Caliburn.Micro;
using Newtonsoft.Json;

namespace Smurferrino.FunctionModels
{
    public abstract class BaseFunctionModel : PropertyChangedBase
    {
        [JsonProperty]
        public abstract string FunctionName { get; set; }

        protected BaseFunctionModel()
        {
            Task.Factory.StartNew(DoWork);
        }

        public abstract void DoWork();
    }
}