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
        public Vector3 NextFreePosition => _formingAreaModel.GetNextFreePosition();

        [Inject] private readonly ScorePresenter _scorePresenter;
        [Inject] private readonly GameStatePresenter _gameStatePresenter;
        
        private readonly LevelPresenter _levelPresenter;
        private readonly IDataStorageService _dataStorageService;

        private readonly FormingAreaModel _formingAreaModel;
        private readonly FormingAreaView _formingAreaView;

        [Inject]
        public FormingAreaPresenter(LevelPresenter levelPresenter,
            IDataStorageService dataStorageService)
        {
            _levelPresenter = levelPresenter;
            _dataStorageService = dataStorageService;

            _formingAreaModel = new FormingAreaModel(levelPresenter);
            _formingAreaView = new FormingAreaView();
        }

        public void Reset() => _formingAreaModel.ResetWord();
        public bool IsAlreadyGiven(string word) => _formingAreaModel.CorrectWords.Contains(word);
        public void AddLetter(LetterTile letterTile) => _formingAreaModel.AddCharacter(letterTile);

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

        public async UniTask SubmitWord()
        {
            _scorePresenter.CalculateScore(_formingAreaModel.CurrentWord);
            var letterTiles = _formingAreaModel.LetterTiles;
            await _formingAreaView.DestroyWord(letterTiles);
            _levelPresenter.ReturnTile(letterTiles);
            _formingAreaModel.AddCurrentWord();
        }

        public async void OnLevelEnd(int remainingLetterCount)
        {
            _scorePresenter.CalculateTotalScore(remainingLetterCount);

            await UpdateLevelData();
            _gameStatePresenter.UpdateGameState(GameState.GameState.LevelEnd);
            _levelPresenter.ReturnTile(_formingAreaModel.FormingTiles);
            _formingAreaModel.ResetWordsAll();
        }

        private async UniTask UpdateLevelData()
        {
            var data = await _dataStorageService.GetFileContentAsync<GameData>();
            var exist = data.levelStatusMap.TryGetValue(_levelPresenter.CurrentLevelIndex, out var levelData);

            levelData.highScore = levelData.highScore < _scorePresenter.Score
                ? _scorePresenter.Score
                : levelData.highScore;
            levelData.playStatus = PlayStatus.Played;

            if (!exist)
                data.levelStatusMap.Add(_levelPresenter.CurrentLevelIndex, levelData);
            else
                data.levelStatusMap[_levelPresenter.CurrentLevelIndex] = levelData;

            _dataStorageService.SetFileContent(data);
        }
    }
}