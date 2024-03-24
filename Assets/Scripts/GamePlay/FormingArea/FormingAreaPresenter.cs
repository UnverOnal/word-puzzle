using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Dictionary;
using GamePlay.Score;
using GamePlay.TileSystem;
using GameState;
using LevelCreation;
using Services.DataStorageService;
using UI.Screens.LevelSelection;
using UnityEngine;
using VContainer;

namespace GamePlay.FormingArea
{
    public class FormingAreaPresenter
    {
        public List<string> CorrectWords => _formingAreaModel.CorrectWords;
        public string Word => _formingAreaModel.CurrentWord;

        private readonly LevelPresenter _levelPresenter;
        private readonly WordDictionary _wordDictionary;
        private readonly ScorePresenter _scorePresenter;
        private readonly GameStatePresenter _gameStatePresenter;
        private readonly IDataStorageService _dataStorageService;

        private readonly FormingAreaModel _formingAreaModel;
        private readonly FormingAreaView _formingAreaView;

        [Inject]
        public FormingAreaPresenter(LevelPresenter levelPresenter, WordDictionary wordDictionary,
            ScorePresenter scorePresenter, GameStatePresenter gameStatePresenter, IDataStorageService dataStorageService)
        {
            _levelPresenter = levelPresenter;
            _wordDictionary = wordDictionary;
            _scorePresenter = scorePresenter;
            _gameStatePresenter = gameStatePresenter;
            _dataStorageService = dataStorageService;

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

        public async void OnLevelEnd(int remainingLetterCount)
        {
            _scorePresenter.CalculateScores(CorrectWords, remainingLetterCount);
            Debug.Log("Score : " + _scorePresenter.Score);
            Debug.Log("Level End");

            await UpdateLevelStatusData();
            _gameStatePresenter.UpdateGameState(GameState.GameState.LevelEnd);
            _levelPresenter.ReturnTile(_formingAreaModel.FormingTiles);
            _formingAreaModel.ResetWordsAll();
        }

        private void DestroyWord()
        {
            var letters = _formingAreaModel.LetterTiles;
            for (var i = 0; i < letters.Count; i++)
            {
                var letterTile = letters[i];
                letterTile.GameObject.SetActive(false);
                _levelPresenter.ReturnTile(letterTile);
            }
        }

        private async UniTask UpdateLevelStatusData()
        {
            var data = await _dataStorageService.GetFileContentAsync<GameData>();
            var isExist = data.levelStatusMap.TryGetValue(_levelPresenter.CurrentLevelIndex, out var levelStatus);
            
            levelStatus.highScore = levelStatus.highScore < _scorePresenter.Score
                ? _scorePresenter.Score
                : levelStatus.highScore;
            levelStatus.playStatus = PlayStatus.Played;

            if (!isExist)
                data.levelStatusMap.Add(_levelPresenter.CurrentLevelIndex, levelStatus);
            else
                data.levelStatusMap[_levelPresenter.CurrentLevelIndex] = levelStatus;
            
            _dataStorageService.SetFileContent(data);
        }
    }
}