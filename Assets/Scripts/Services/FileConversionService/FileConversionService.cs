using System;
using Services.FileConversionService.Converters;
using UnityEngine;

namespace Services.FileConversionService
{
    public class FileConversionService : IFileConversionService
    {
        public IConverter GetConverter(ConverterType type, TextAsset textAsset)
        {
            switch (type)
            {
                case ConverterType.JsonConverter:
                    return new JsonConverter(textAsset);
                case ConverterType.CsvConverter:
                    return new CsvConverter(textAsset);
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}
