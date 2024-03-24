using System.Collections.Generic;
using Dictionary;
using GamePlay.TileSystem;
using LevelCreation;
using UnityEngine;
using VContainer;

namespace GamePlay.FormingArea
{
    public class FormingAreaPresenter
    {
        public List<string> CorrectWords => _formingAreaModel.CorrectWords;
        public string Word => _formingAreaModel.CurrentWord;

        private readonly WordDictionary _wordDictionary;

        private readonly FormingAreaModel _formingAreaModel;
        private readonly FormingAreaView _formingAreaView;

        [Inject]
        public FormingAreaPresenter(LevelPresenter levelPresenter, WordDictionary wordDictionary)
        {
            _wordDictionary = wordDictionary;

            _formingAreaModel = new FormingAreaModel(levelPresenter);
            _formingAreaView = new FormingAreaView();
        }

        public void AddLetter(LetterTile letterTile)
        {
            _formingAreaModel.AddCharacter(letterTile);
        }

        public LetterTile TakeLetter()
        {
            var letter = _formingAreaModel.LetterTiles[^1];
            _formingAreaModel.RemoveCharacter();
            return letter;
        }

        public List<LetterTile> TakeLetterAll()
        {
            var letters = new List<LetterTile>(_formingAreaModel.LetterTiles);
            Reset();
            return letters;
        }

        public void Reset()
        {
            _formingAreaModel.ResetWord();
        }

        public Vector3 GetNextFreePosition()
        {
            var nextTile = _formingAreaModel.FormingTiles[_formingAreaModel.OccupiedIndex];
            var position = nextTile.GameObject.transform.position;
            return position;
        }

        public void SubmitWord()
        {
            DestroyWord();
            _formingAreaModel.AddCurrentWord();
        }

        private void DestroyWord()
        {
            var letters = _formingAreaModel.LetterTiles;
            for (var i = 0; i < letters.Count; i++)
            {
                var letterTile = letters[i];
                letterTile.GameObject.SetActive(false);
            }
        }
    }
}