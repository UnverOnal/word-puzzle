using Dictionary;
using GamePlay.TileSystem;
using LevelCreation;
using UnityEngine;
using VContainer;

namespace GamePlay.FormingArea
{
    public class FormingAreaPresenter
    {
        public string Word => _formingAreaModel.Word;
        
        private readonly DictionaryPreprocessor _dictionaryPreprocessor;

        private readonly FormingAreaModel _formingAreaModel;
        private readonly FormingAreaView _formingAreaView;

        [Inject]
        public FormingAreaPresenter(LevelPresenter levelPresenter, DictionaryPreprocessor dictionaryPreprocessor)
        {
            _dictionaryPreprocessor = dictionaryPreprocessor;

            _formingAreaModel = new FormingAreaModel(levelPresenter);
            _formingAreaView = new FormingAreaView();
        }

        public void AddLetter(LetterTile letterTile)
        {
            _formingAreaModel.AddCharacter(letterTile);
        }

        public void RemoveLetter()
        {
            _formingAreaModel.RemoveCharacter();
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

        public void DestroyTiles()
        {
            var letters = _formingAreaModel.LetterTiles;
            for (int i = 0; i < letters.Count; i++)
            {
                var letterTile = letters[i];
                letterTile.GameObject.SetActive(false);
            }
        }
    }
}
