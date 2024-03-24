using System.Collections.Generic;
using GamePlay.TileSystem;
using LevelCreation;

namespace GamePlay.FormingArea
{
    public class FormingAreaModel
    {
        public List<BlankTile> FormingTiles => _levelPresenter.FormingTiles;
        public List<LetterTile> LetterTiles { get; private set; }
        public List<string> CorrectWords { get; private set; }
        public string CurrentWord { get; private set; }
        public int OccupiedIndex { get; private set; }

        private readonly LevelPresenter _levelPresenter;

        public FormingAreaModel(LevelPresenter levelPresenter)
        {
            _levelPresenter = levelPresenter;

            LetterTiles = new List<LetterTile>();
            CorrectWords = new List<string>();
        }

        public void AddCharacter(LetterTile letterTile)
        {
            LetterTiles.Add(letterTile);
            CurrentWord += char.Parse(letterTile.Character);
            OccupiedIndex++;
        }

        public void RemoveCharacter()
        {
            if (string.IsNullOrEmpty(CurrentWord))
                return;

            LetterTiles.Remove(LetterTiles[^1]);
            CurrentWord = CurrentWord[..^1];
            OccupiedIndex--;
        }

        public void ResetWord()
        {
            LetterTiles.Clear();
            CurrentWord = string.Empty;
            OccupiedIndex = 0;
        }

        public void AddCurrentWord()
        {
            CorrectWords.Add(CurrentWord);
        }

        public void ResetWordsAll()
        {
            CorrectWords.Clear();
        }
    }
}