using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dictionary
{
    [CreateAssetMenu(fileName = "NewDictionary", menuName = "ScriptableObjects/NewDictionary")]
    public class DictionaryData : ScriptableObject
    {
        [SerializeField] private TextAsset dictionaryTextFile;
        [HideInInspector] public int minimumWordSize = int.MaxValue;

        public List<string> DictionaryWords { get; private set; }

        public void ConvertDictionaryToList()
        {
            DictionaryWords = new List<string>();

            var words = dictionaryTextFile.text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            for (var i = 0; i < words.Length; i++)
            {
                var word = words[i];
                DictionaryWords.Add(word.Trim());
                CalculateMinimumWordSize(word);
            }
        }

        private void CalculateMinimumWordSize(string word)
        {
            if (minimumWordSize > word.Length)
                minimumWordSize = word.Length;
        }
    }
}