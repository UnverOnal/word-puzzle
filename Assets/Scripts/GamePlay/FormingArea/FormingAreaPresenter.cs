using System.Collections.Generic;
using Cysharp.Threading.Tasks;
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
        public string Word => _formingAreaModel.CurrentWord;

        private readonly LevelPresenter _levelPresenter;
        private readonly ScorePresenter _scorePresenter;
        private readonly GameStatePresenter _gameStatePresenter;
        private readonly IDataStorageService _dataStorageService;

        private readonly FormingAreaModel _formingAreaModel;
        private readonly FormingAreaView _formingAreaView;

        [Inject]
        public FormingAreaPresenter(LevelPresenter levelPresenter,
            ScorePresenter scorePresenter, GameStatePresenter gameStatePresenter, IDataStorageService dataStorageService)
        {
            _levelPresenter = levelPresenter;
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

        public async UniTask SubmitWord()
        {
            _scorePresenter.CalculateScore(_formingAreaModel.CurrentWord);
            await DestroyWord();
            _formingAreaModel.AddCurrentWord();
        }

        public async void OnLevelEnd(int remainingLetterCount)
        {
            _scorePresenter.CalculateTotalScore(remainingLetterCount);

            await UpdateLevelStatusData();
            _gameStatePresenter.UpdateGameState(GameState.GameState.LevelEnd);
            _levelPresenter.ReturnTile(_formingAreaModel.FormingTiles);
            _formingAreaModel.ResetWordsAll();
        }

        public bool IsAlreadyGiven(string word)
        {
            return _formingAreaModel.CorrectWords.Contains(word);
        }

        private async UniTask DestroyWord()
        {
            var letters = _formingAreaModel.LetterTiles;
            await _formingAreaView.DestroyWord(letters);
            _levelPresenter.ReturnTile(letters);
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