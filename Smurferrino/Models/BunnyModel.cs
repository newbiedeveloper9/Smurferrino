using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Newtonsoft.Json;
using Smurferrino.Serialize;

namespace Smurferrino.Models
{
    public class BunnyModel : BaseFunctionModel
    {
        public override string FunctionName { get; set; } = "Bunny";
        public override void DoWork()
        {
            throw new NotImplementedException();
        }

        private bool _enabled;

        [JsonProperty]
        public bool Enabled
        {
            get => _enabled;
            set
            {
                if (_enabled == value) return;
                _enabled = value;
                NotifyOfPropertyChange(() => Enabled);
            }
        }
    }
}
