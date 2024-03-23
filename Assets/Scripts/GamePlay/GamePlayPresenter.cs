using System;
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
        private readonly IInputService _inputService;
        private readonly CommandInvoker _commandInvoker;
        private readonly LevelPresenter _levelPresenter;
        private readonly DictionaryPreprocessor _dictionaryPreprocessor;
        private readonly FormingAreaPresenter _formingAreaPresenter;

        private readonly GamePlayModel _gamePlayModel;

        private readonly ObjectPool<MoveCommand> _moveCommandPool;

        [Inject]
        public GamePlayPresenter(IInputService inputService, IPoolService poolService, ICommandService commandService,
            LevelPresenter levelPresenter, GameSettings gameSettings, DictionaryPreprocessor dictionaryPreprocessor, FormingAreaPresenter formingAreaPresenter)
        {
            _inputService = inputService;
            _commandInvoker = commandService.GetCommandInvoker();
            _levelPresenter = levelPresenter;
            _dictionaryPreprocessor = dictionaryPreprocessor;
            _formingAreaPresenter = formingAreaPresenter;

            _gamePlayModel = new GamePlayModel();

            _moveCommandPool = poolService.GetPoolFactory().CreatePool(() => new MoveCommand(gameSettings.moveData));
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
            var moveCommand = _moveCommandPool.Get();

            var targetPosition = _formingAreaPresenter.GetNextFreePosition();
            _formingAreaPresenter.AddLetter(tile);
            moveCommand.SetMoveData(tile, targetPosition);

            _commandInvoker.ExecuteCommand(moveCommand);
        }

        public void Undo()
        {
            _commandInvoker.UndoCommand();
            _formingAreaPresenter.RemoveLetter();
        }

        public void UndoAll()
        {
            _commandInvoker.UndoCommandAll();
            _formingAreaPresenter.Reset();
        }

        public void Submit()
        {
            var isWordCorrect = _dictionaryPreprocessor.ContainsWord(_formingAreaPresenter.Word);
            
            if(!isWordCorrect)
                _commandInvoker.UndoCommandAll();
            else
            {
                _commandInvoker.Reset();
                _formingAreaPresenter.DestroyTiles();
            }
            
            //Returns commands to the pool
            while (_commandInvoker.Commands.Count > 0)
                _moveCommandPool.Return((MoveCommand)_commandInvoker.Commands.Pop());
                
            _formingAreaPresenter.Reset();
        }

        private LetterTile GetSelectedTile(GameObject gameObject)
        {
            var tiles = _levelPresenter.Tiles;
            for (int i = 0; i < tiles.Count; i++)
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