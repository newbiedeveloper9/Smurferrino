using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using Darc_Euphoria.Euphoric;
using Smurferrino.Business.Enums;
using Smurferrino.Business.Helpers;
using Smurferrino.Business.Memory;
using Smurferrino.Helpers;

namespace Smurferrino.FunctionModels
{
    public class TestModel : BaseFunctionModel
    {
        public override string FunctionName { get; set; } = "Test";

        public override void DoWork()
        {
            /* while (true)
             {
                 TestCollection[0].Value = StopwatchHelper.average.ToString();
                 TestCollection[1].Value = StopwatchHelper.iterate.ToString();

                 if (StopwatchHelper.iterate >= 1000 && string.IsNullOrEmpty(TestCollection[2].Value))
                     TestCollection[2].Value = StopwatchHelper.value.ToString();
                 if (StopwatchHelper.iterate >= 3000 && string.IsNullOrEmpty(TestCollection[3].Value))
                     TestCollection[3].Value = StopwatchHelper.value.ToString();

                 Thread.Sleep(2);
             }*/
            while (true)
            {
                if (Global.LocalPlayer == null || Global.ProcessState != ProcessState.Attached)
                {
                    Thread.Sleep(1000);
                    continue;
                }

                if (Keyboard.IsPressed(164))
                    TestCollection[0].Value = Global.LocalPlayer.PunchAngle.ToString();

                Thread.Sleep(10);
            }
        }

        public TestModel()
        {
            TestCollection = new BindableCollection<TestObj>()
            {
                /*new TestObj("Average"),
                new TestObj("Count"),
                new TestObj("1000"),
                new TestObj("3000"),*/

                new TestObj("Punch")
            };
        }

        public void Stop()
        {
            //Global.LocalPlayer.SendPackets = false;
            Global.LocalPlayer.ThirdPerson = !Global.LocalPlayer.ThirdPerson;
        }

        public void Update()
        {
           // Global.LocalPlayer.ForceUpdate();
        }

        private BindableCollection<TestObj> _testCollection;
        public BindableCollection<TestObj> TestCollection
        {
            get => _testCollection;
            set
            {
                if (_testCollection == value) return;
                _testCollection = value;
                NotifyOfPropertyChange(() => TestCollection);
            }
        }

        public void Reset()
        {
            foreach (var testObj in TestCollection)
            {
                testObj.Value = "";
            }

            StopwatchHelper.value = 0;
            StopwatchHelper.iterate = 0;
        }
    }

    public class TestObj : PropertyChangedBase
    {
        public TestObj(string name)
        {
            _name = name;
        }

        private string _name;
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

        private string _value;
        public string Value
        {
            get => _value;
            set
            {
                if (_value == value) return;
                _value = value;
                NotifyOfPropertyChange(() => Value);
            }
        }
    }
}
