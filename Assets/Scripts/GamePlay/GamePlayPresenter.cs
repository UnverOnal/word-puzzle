using System;
using Dictionary;
using GameManagement;
using GamePlay.FormingArea;
using GamePlay.Score;
using GamePlay.TileSystem;
using LevelCreation;
using Services.CommandService;
using Services.InputService;
using Services.PoolingService;
using UnityEngine;
using VContainer;

namespace GamePlay
{
    public class GamePlayPresenter : IDisposable
    {
        private readonly IInputService _inputService;
        private readonly CommandInvoker _commandInvoker;
        private readonly LevelPresenter _levelPresenter;
        private readonly WordDictionary _wordDictionary;
        private readonly FormingAreaPresenter _formingAreaPresenter;
        private readonly ScorePresenter _scorePresenter;

        private readonly GamePlayModel _gamePlayModel;
        private readonly PossibleMoveTracker _possibleMoveTracker;

        private readonly ObjectPool<MoveCommand> _moveCommandPool;

        [Inject]
        public GamePlayPresenter(IInputService inputService, IPoolService poolService, ICommandService commandService,
            LevelPresenter levelPresenter, GameSettings gameSettings, WordDictionary wordDictionary,
            FormingAreaPresenter formingAreaPresenter, ScorePresenter scorePresenter)
        {
            _inputService = inputService;
            _commandInvoker = commandService.GetCommandInvoker();
            _levelPresenter = levelPresenter;
            _wordDictionary = wordDictionary;
            _formingAreaPresenter = formingAreaPresenter;
            _scorePresenter = scorePresenter;

            _gamePlayModel = new GamePlayModel(_levelPresenter);

            _moveCommandPool = poolService.GetPoolFactory().CreatePool(() => new MoveCommand(gameSettings.moveData));

            _possibleMoveTracker = new PossibleMoveTracker(wordDictionary);
        }

        public void Initialize()
        {
            _inputService.OnItemPicked += OnTileSelected;
        }

        private void OnTileSelected(GameObject selectedObject)
        {
            if (selectedObject.layer != LayerMask.NameToLayer("Tile"))
                return;

            var tile = GetSelectedTile(selectedObject);
            MoveLetter(tile);
        }

        private void MoveLetter(LetterTile tile)
        {
            var moveCommand = _moveCommandPool.Get();

            var targetPosition = _formingAreaPresenter.GetNextFreePosition();
            moveCommand.SetMoveData(tile, targetPosition);
            _commandInvoker.ExecuteCommand(moveCommand);

            _formingAreaPresenter.AddLetter(tile);
            _gamePlayModel.RemoveTile(tile);
        }

        public void Undo()
        {
            _commandInvoker.UndoCommand();
            var letter = _formingAreaPresenter.TakeLetter();
            _gamePlayModel.AddTile(letter);
        }

        public void UndoAll()
        {
            _commandInvoker.UndoCommandAll();
            var letters = _formingAreaPresenter.TakeLetterAll();
            _gamePlayModel.AddTiles(letters);
        }

        public void Submit()
        {
            var isWordCorrect = _wordDictionary.ContainsWord(_formingAreaPresenter.Word);

            if (!isWordCorrect)
                UndoAll();
            else
            {
                _commandInvoker.Reset();
                _formingAreaPresenter.SubmitWord();
            }

            // Returns commands to the pool
             while (_commandInvoker.Commands.Count > 0)
                 _moveCommandPool.Return((MoveCommand)_commandInvoker.Commands.Pop());

            _formingAreaPresenter.Reset();

            var isLevelEnd = !_possibleMoveTracker.IsPossible(_gamePlayModel.Tiles) || _gamePlayModel.Tiles.Count < 1;
            if (isLevelEnd)
            {
                var remainingLetterCount = _gamePlayModel.Tiles.Count;
                _scorePresenter.CalculateScores(_formingAreaPresenter.CorrectWords, remainingLetterCount);
                Debug.Log("Score : " + _scorePresenter.Score);
                Debug.Log("Level End");
            }
        }

        private LetterTile GetSelectedTile(GameObject gameObject)
        {
            var tiles = _levelPresenter.Tiles;
            for (var i = 0; i < tiles.Count; i++)
            {
                var tile = tiles[i];
                if (tile.IsSame(gameObject))
                    return tile;
            }

            return null;
        }

        public void Dispose()
        {
            _inputService.OnItemPicked -= OnTileSelected;
        }
    }
}