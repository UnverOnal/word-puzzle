using System.Collections.Generic;
using GamePlay.TileSystem;
using LevelCreation;

namespace GamePlay.FormingArea
{
    public class FormingAreaModel
    {
        public List<BlankTile> FormingTiles => _levelPresenter.FormingTiles;
        public List<LetterTile> LetterTiles { get; private set; }
        public string Word { get; private set; }
        public int OccupiedIndex { get; private set; }

        private readonly LevelPresenter _levelPresenter;

        public FormingAreaModel(LevelPresenter levelPresenter)
        {
            _levelPresenter = levelPresenter;

            LetterTiles = new List<LetterTile>();
        }

        public void AddCharacter(LetterTile letterTile)
        {
            LetterTiles.Add(letterTile);
            Word += char.Parse(letterTile.Character);
            OccupiedIndex++;
        }

        public void RemoveCharacter()
        {
            if (string.IsNullOrEmpty(Word))
                return;

            LetterTiles.Remove(LetterTiles[^1]);
            Word = Word[..^1];
            OccupiedIndex--;
        }

        public void ResetWord()
        {
            LetterTiles.Clear();
            Word = string.Empty;
            OccupiedIndex = 0;
        }
    }
}