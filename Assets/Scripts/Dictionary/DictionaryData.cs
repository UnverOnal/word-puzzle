using System.Collections.Generic;
using UnityEngine;

namespace Dictionary
{
    [CreateAssetMenu(fileName = "NewDictionary", menuName = "ScriptableObjects/NewDictionary")]
    public class DictionaryData : ScriptableObject
    {
        [SerializeField] private TextAsset dictionaryTextFile; 
        [HideInInspector]public bool dictionaryConverted;

        public List<string> DictionaryWords { get; private set; }

        public void ConvertDictionaryToList()
        {
            if (dictionaryTextFile == null)
            {
                Debug.LogError("Dictionary text file is not assigned!");
                return;
            }

            DictionaryWords = new List<string>();

            var words = dictionaryTextFile.text.Split(new char[] { '\r', '\n' }, System.StringSplitOptions.RemoveEmptyEntries);

            for (var i = 0; i < words.Length; i++)
            {
                var word = words[i];
                DictionaryWords.Add(word.Trim());
            }

            dictionaryConverted = true;
            Debug.Log("Dictionary converted to list. Word count: " + DictionaryWords.Count);
        }
    }
}