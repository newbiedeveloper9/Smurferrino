using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Caliburn.Micro;
using Newtonsoft.Json;
using Smurferrino.Models;
using Smurferrino.Serialize;

namespace Smurferrino
{
    public static class ModelHelper
    {
        private static string _configHash = string.Empty;
        private static List<FunctionModel> _configSource = new List<FunctionModel>();

        private static JsonSerializerSettings serializeSettings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.All
        };

        public static void SaveToFileByRAM(this List<FunctionModel> list, string fileName)
        {
            List<FunctionModel> listToSave = list.Where(x => !string.IsNullOrWhiteSpace(x.Json)).ToList();
            serializeSettings.Formatting = Formatting.Indented;
            string output = JsonConvert.SerializeObject(listToSave, serializeSettings);

            var finallyPath = $"{FilePaths.JsonDirectoryPath}{fileName}.json";
            File.WriteAllText(finallyPath, output);

            FunctionModelSingleton.Instance.FunctionModels.ClearFromRAM();
        }

        private static void ClearFromRAM(this List<FunctionModel> list)
        {
            foreach (var model in list.Where(x => !string.IsNullOrWhiteSpace(x.Json)))
                model.Json = string.Empty;
        }

        public static void SaveModelRAM(this IModel model)
        {
            serializeSettings.Formatting = Formatting.Indented;

            var functionModel = FunctionModelSingleton.Instance.FunctionModels.GetByModel(model);
            functionModel.Json = JsonConvert.SerializeObject(model, typeof(IModel), serializeSettings);
        }

        public static IModel LoadModel(this IModel model, string fileName)
        {
            var filePath = $"{FilePaths.JsonDirectoryPath}{fileName}.json";

            var fileHash = Hash.CalculateMD5(filePath);
            if (string.IsNullOrWhiteSpace(_configHash) || _configHash != fileHash)
            {
                var source = File.ReadAllText(filePath);
                _configSource = JsonConvert.DeserializeObject<List<FunctionModel>>(source, serializeSettings);
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

        public static FunctionModel GetByModel(this List<FunctionModel> list, IModel model)
        {
            return list.GetByFunctionName(model.FunctionName);
        }
    }
}
