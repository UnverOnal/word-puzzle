using System.Collections.Generic;
using System.Linq;
using VContainer;

namespace Dictionary
{
    public class WordDictionary
    {
        public int MinimumWordSize => _data.minimumWordSize;
        
        private readonly DictionaryData _data;
        private HashSet<string> _wordHashSet;

        [Inject]
        public WordDictionary(DictionaryData data)
        {
            _data = data;
        }

        public void Initialize()
        {
            _data.ConvertDictionaryToList();
            var words = _data.DictionaryWords;
            _wordHashSet = new HashSet<string>(words.Select(word => word.ToLower()));
        }

        public bool ContainsWord(string word)
        {
            return _wordHashSet.Contains(word.ToLower());
        }
    }
}