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
    public class BunnyModel : PropertyChangedBase, IModel
    {
        [JsonProperty]
        public string FunctionName { get; set; } = "Bunny";

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
