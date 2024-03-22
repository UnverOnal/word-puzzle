using UnityEngine;

namespace Services.FileConversionService.Converters
{
    public class Converter
    {
        protected readonly string fileData;

        protected Converter(TextAsset textAsset)
        {
            fileData = textAsset.text;
        }
    }
}
