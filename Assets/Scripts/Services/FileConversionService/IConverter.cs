using System.Collections.Generic;

namespace Services.FileConversionService
{
    public interface IConverter
    {
        public List<T> GetDataCollection<T>()where T : new();
        public T GetData<T>() where T : new();
    }
}