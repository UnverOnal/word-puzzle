using System.Collections.Generic;
using Dictionary;
using GamePlay.TileSystem;

namespace GamePlay
{
    public class PossibleMoveTracker
    {
        private readonly WordDictionary _wordDictionary;

        public PossibleMoveTracker(WordDictionary wordDictionary)
        {
            _wordDictionary = wordDictionary;
        }

        public bool IsPossible(List<LetterTile> tiles)
        {
            var canCalculate = tiles.Count <= _wordDictionary.MinimumWordSize + 1;
            if (!canCalculate)
                return true;
            
            var letters = GetChars(tiles);
            var combinations = GenerateCombinations(letters);
            
            for (int i = 0; i < combinations.Count; i++)
            {
                var combination = combinations[i];
                if (_wordDictionary.ContainsWord(combination))
                    return true;
            }

            return false;
        }

        private char[] GetChars(List<LetterTile> tiles)
        {
            var letters = new char[tiles.Count];
            for (int i = 0; i < tiles.Count; i++)
                letters[i] = char.Parse(tiles[i].Character);

            return letters;
        }

        private List<string> GenerateCombinations(char[] chars)
        {
            var result = new List<string>();
            GenerateCombinations(chars, "",new List<int>(), result);
            return result;
        }

        private void GenerateCombinations(char[] chars, string current, List<int> currentIndices, List<string> result)
        {
            switch (current.Length)
            {
                case > 7:
                    return;
                case > 1:
                    if(!result.Contains(current))
                        result.Add(current);
                    break;
            }

            for (int i = 0; i < chars.Length; i++)
            {
                if (!currentIndices.Contains(i))
                {
                    var next = new List<int>(currentIndices) { i };
                    GenerateCombinations(chars, current + chars[i], next, result);
                }
            }
        }
    }
}
