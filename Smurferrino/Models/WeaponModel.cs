using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Newtonsoft.Json;

namespace Smurferrino.Models
{
    public class WeaponModel : PropertyChangedBase
    {
        public WeaponModel(int id, string name)
        {
            _id = id;
            _name = name;
        }

        public WeaponModel()
        {
            
        }

        private int _id;
        [JsonProperty]
        public int Id
        {
            get => _id;
            set
            {
                if (_id == value) return;
                _id = value;
                NotifyOfPropertyChange(() => Id);
            }
        }

        private string _name;
        [JsonProperty]
        public string Name
        {
            get => _name;
            set
            {
                if (_name == value) return;
                _name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }

        private int _preSprayDelay;
        [JsonProperty]
        public int PreSprayDelay
        {
            get => _preSprayDelay;
            set
            {
                if (_preSprayDelay == value) return;
                _preSprayDelay = value;
                NotifyOfPropertyChange(() => PreSprayDelay);
            }
        }

        private int _sprayDuration;
        [JsonProperty]
        public int SprayDuration
        {
            get => _sprayDuration;
            set
            {
                if (_sprayDuration == value) return;
                _sprayDuration = value;
                NotifyOfPropertyChange(() => SprayDuration);
            }
        }

        private int _afterSprayDelay;
        [JsonProperty]
        public int AfterSprayDelay
        {
            get => _afterSprayDelay;
            set
            {
                if (_afterSprayDelay == value) return;
                _afterSprayDelay = value;
                NotifyOfPropertyChange(() => AfterSprayDelay);

            }
        }
    }
}
