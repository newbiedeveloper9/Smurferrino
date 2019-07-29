using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Caliburn.Micro;
using Newtonsoft.Json;
using Smurferrino.FunctionModels;
using Smurferrino.Serialize;

namespace Smurferrino
{
    public static class ModelHelper
    {
        private static string _configHash = string.Empty;
        private static List<FunctionModel> _configSource = new List<FunctionModel>();

        private static readonly JsonSerializerSettings SerializeSettings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.All
        };


        public static void SaveToFileByRAM(this List<FunctionModel> list, string fileName)
        {
            List<FunctionModel> listToSave = list.Where(x => !string.IsNullOrWhiteSpace(x.Json)).ToList();
            SerializeSettings.Formatting = Formatting.Indented;
            string output = JsonConvert.SerializeObject(listToSave, SerializeSettings);

            var filePath = $"{FilePaths.JsonDirectoryPath}{fileName}";
            if (!filePath.Contains(".json"))
                filePath += ".json";

            File.WriteAllText(filePath, output);

            FunctionModelSingleton.Instance.FunctionModels.ClearRAM();
        }

        private static void ClearRAM(this List<FunctionModel> list)
        {
            foreach (var model in list.Where(x => !string.IsNullOrWhiteSpace(x.Json)))
                model.Json = string.Empty;
        }

        public static void SaveModelRAM(this BaseFunctionModel model)
        {
            SerializeSettings.Formatting = Formatting.Indented;

            var functionModel = FunctionModelSingleton.Instance.FunctionModels.GetByModel(model);
            functionModel.Json = JsonConvert.SerializeObject(model, typeof(BaseFunctionModel), SerializeSettings);
        }

        public static BaseFunctionModel LoadModel(this BaseFunctionModel model, string fileName)
        {
            var filePath = $"{FilePaths.JsonDirectoryPath}{fileName}";
            if (!filePath.Contains(".json"))
                filePath += ".json";

            var fileHash = Hash.CalculateMD5(filePath);
            if (string.IsNullOrWhiteSpace(_configHash) || _configHash != fileHash)
            {
                var source = File.ReadAllText(filePath);
                _configSource = JsonConvert.DeserializeObject<List<FunctionModel>>(source, SerializeSettings);
                _configHash = fileHash;
            }

            var newFunctionModel = _configSource.GetByModel(model);
            var oldFunctionModel = FunctionModelSingleton.Instance.FunctionModels.GetByModel(model);

            oldFunctionModel.Json = string.Empty;
            oldFunctionModel.Model = newFunctionModel.Model;

            return oldFunctionModel.Model;
        }

        public static FunctionModel GetByFunctionName(this List<FunctionModel> list, string functionName)
        {
            return list.FirstOrDefault(x => x.Model.FunctionName.ToLower().Equals(functionName.ToLower()));
        }

        public static FunctionModel GetByModel(this List<FunctionModel> list, BaseFunctionModel model)
        {
            return list.GetByFunctionName(model.FunctionName);
        }
    }
}
