using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smurferrino.FunctionModels;
using Smurferrino.Interfaces;
using Smurferrino.Serialize;
using Smurferrino.Services;

namespace Smurferrino
{
    public sealed class FunctionModelSingleton
    {
        private static readonly Lazy<FunctionModelSingleton> lazy =
            new Lazy<FunctionModelSingleton>(() => new FunctionModelSingleton());

        public static FunctionModelSingleton Instance => lazy.Value;

        private FunctionModelSingleton()
        {
            IRcs recoilSys = new NoRecoil();

            FunctionModels = new List<FunctionModel>()
            {
                new FunctionModel(new BunnyModel()),
                new FunctionModel(new TriggerModel(recoilSys)),
                new FunctionModel(new GlowModel()),
                new FunctionModel(new VisualsModel()),
                new FunctionModel(new CompSpecsModel()),
                new FunctionModel(new RCSModel(recoilSys)),
            };
        }


        public List<FunctionModel> FunctionModels { get; set; }
    }
}
