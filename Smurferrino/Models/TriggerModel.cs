using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Newtonsoft.Json;

namespace Smurferrino.Models
{
    public class TriggerModel : PropertyChangedBase, IModel
    {
        [JsonProperty]
        public string FunctionName { get; set; } = "Trigger";

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
