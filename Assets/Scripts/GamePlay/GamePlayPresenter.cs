using System;
using Cysharp.Threading.Tasks;
using Dictionary;
using GameManagement;
using GamePlay.FormingArea;
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
        public event Action<bool> OnMoveCommand; 

        [Inject] private readonly IInputService _inputService;
        [Inject] private readonly IPoolService _poolService;
        [Inject] private readonly FormingAreaPresenter _formingAreaPresenter;

        private readonly GameSettings _gameSettings;
        private readonly CommandInvoker _commandInvoker;
        private readonly LevelPresenter _levelPresenter;
        private readonly WordDictionary _wordDictionary;

        private readonly GamePlayModel _gamePlayModel;
        private readonly GamePlayView _gamePlayView;
        private readonly PossibleMoveTracker _possibleMoveTracker;

        private ObjectPool<MoveCommand> _moveCommandPool;

        [Inject]
        public GamePlayPresenter(ICommandService commandService,
            LevelPresenter levelPresenter, WordDictionary wordDictionary, GameSettings gameSettings)
        {
            _commandInvoker = commandService.GetCommandInvoker();
            _levelPresenter = levelPresenter;
            _wordDictionary = wordDictionary;
            _gameSettings = gameSettings;

            _gamePlayModel = new GamePlayModel(levelPresenter);
            _gamePlayView = new GamePlayView(gameSettings);

            _possibleMoveTracker = new PossibleMoveTracker(wordDictionary);
        }

        public void Initialize()
        {
            _moveCommandPool = _poolService.GetPoolFactory().CreatePool(() => new MoveCommand(_gameSettings.moveData));

            _inputService.OnItemPicked += OnTileSelected;
        }

        private void OnTileSelected(GameObject selectedObject)
        {
            if (selectedObject.layer != LayerMask.NameToLayer("Tile"))
                return;

            if (_formingAreaPresenter.IsFull())
            {
                _gamePlayView.VibrateTile(selectedObject.transform);
                return;
            }

            var tile = GetSelectedTile(selectedObject);
            MoveLetter(tile);
        }

        private void MoveLetter(LetterTile tile)
        {
            var moveCommand = _moveCommandPool.Get();

            var targetPosition = _formingAreaPresenter.NextFreePosition;
            moveCommand.SetMoveData(tile, targetPosition);
            _commandInvoker.ExecuteCommand(moveCommand);

            _formingAreaPresenter.AddLetter(tile);
            _gamePlayModel.RemoveTile(tile);
            
            OnMoveCommand?.Invoke(_commandInvoker.undoStack.Count > 0);
        }

        public void Undo()
        {
            _commandInvoker.UndoCommand();
            var letter = _formingAreaPresenter.TakeLetter();
            _gamePlayModel.AddTile(letter);
            OnMoveCommand?.Invoke(_commandInvoker.undoStack.Count > 0);
        }

        public void UndoAll()
        {
            GetLettersBack();
            _commandInvoker.UndoCommandAll();
            OnMoveCommand?.Invoke(_commandInvoker.undoStack.Count > 0);
        }

        public async void Submit()
        {
            var word = _formingAreaPresenter.Word;
            var isWordCorrect = _wordDictionary.ContainsWord(word) && !_formingAreaPresenter.IsAlreadyGiven(word);

            if (!isWordCorrect)
                await OnWrongSubmit();
            else
            {
                _commandInvoker.Reset();
                await _formingAreaPresenter.SubmitWord();
            }

            ReturnCommandsToPool();
            _formingAreaPresenter.Reset();
            CheckLevelEnd();
        }

        private void ReturnCommandsToPool()
        {
            while (_commandInvoker.Commands.Count > 0)
                _moveCommandPool.Return((MoveCommand)_commandInvoker.Commands.Pop());
            _commandInvoker.Reset();
            OnMoveCommand?.Invoke(_commandInvoker.undoStack.Count > 0);
        }

        private void CheckLevelEnd()
        {
            var isLevelEnd = !_possibleMoveTracker.IsPossible(_gamePlayModel.Tiles) || _gamePlayModel.Tiles.Count < 1;
            if (isLevelEnd)
            {
                _formingAreaPresenter.OnLevelEnd(_gamePlayModel.Tiles.Count);
                _levelPresenter.ReturnTile(_gamePlayModel.Tiles);
                _gamePlayModel.Reset();
            }
        }

        private async UniTask OnWrongSubmit()
        {
            GetLettersBack();
            await _gamePlayView.Vibrate(_gamePlayModel.WrongTiles);
            _commandInvoker.UndoCommandAll();
        }

        private void GetLettersBack()
        {
            var letters = _formingAreaPresenter.TakeLetterAll();
            _gamePlayModel.AddWrongTiles(letters);
            _gamePlayModel.AddTiles(letters);
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