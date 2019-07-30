using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Newtonsoft.Json;
using Smurferrino.Enums;
using Smurferrino.Helpers;

namespace Smurferrino.FunctionModels
{
    public class CompSpecsModel : BaseFunctionModel
    {
        public override string FunctionName { get; set; } = "CompSpecs";

        public override void DoWork()
        { }

        [JsonProperty]
        private ComputerSpec _userSpec = ComputerSpec.Normal;
        public ComputerSpec UserSpec
        {
            get => _userSpec;
            set
            {
                if (_userSpec == value) return;
                _userSpec = value;
                NotifyOfPropertyChange(() => UserSpec);

                ThreadSleep.Specs = value;
            }
        }

    }
}
