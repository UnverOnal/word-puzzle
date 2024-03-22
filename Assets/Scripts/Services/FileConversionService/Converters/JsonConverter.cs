using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Services.FileConversionService.Converters
{
    public class JsonConverter : Converter, IConverter
    {
        public JsonConverter(TextAsset textAsset) : base(textAsset)
        {
        }

        public List<T> GetDataCollection<T>() where T : new()
        {
            var dataCollection = GetData<List<T>>();
            return dataCollection;
        }
        
        public T GetData<T>() where T : new()
        {
            var content = fileData;
            var convertedObject = JsonConvert.DeserializeObject<T>(content);
            return convertedObject;
        }
    }
}