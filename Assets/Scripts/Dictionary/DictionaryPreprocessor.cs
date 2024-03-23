using System.Collections.Generic;
using System.Linq;
using VContainer;

namespace Dictionary
{
    public class DictionaryPreprocessor
    {
        private readonly DictionaryData _data;
        private HashSet<string> _wordHashSet;

        [Inject]
        public DictionaryPreprocessor(DictionaryData data)
        {
            _data = data;
        }

        public void Initialize()
        {
            var words = _data.DictionaryWords;
            _wordHashSet = new HashSet<string>(words.Select(word => word.ToLower()));
        }

        public bool ContainsWord(string word)
        {
            return _wordHashSet.Contains(word.ToLower());
        }
    }
}