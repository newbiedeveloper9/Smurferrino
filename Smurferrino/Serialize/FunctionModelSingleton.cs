﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smurferrino.FunctionModels;
using Smurferrino.Serialize;

namespace Smurferrino
{
    public sealed class FunctionModelSingleton
    {
        private static readonly Lazy<FunctionModelSingleton> lazy =
            new Lazy<FunctionModelSingleton>(() => new FunctionModelSingleton());

        public static FunctionModelSingleton Instance => lazy.Value;

        private FunctionModelSingleton()
        {
            FunctionModels = new List<FunctionModel>()
            {
                new FunctionModel(new BunnyModel()),
                new FunctionModel(new TriggerModel()),
                new FunctionModel(new GlowModel()),
                new FunctionModel(new VisualsModel()),
                new FunctionModel(new CompSpecsModel())
            };
        }


        public List<FunctionModel> FunctionModels { get; set; }
    }
}
