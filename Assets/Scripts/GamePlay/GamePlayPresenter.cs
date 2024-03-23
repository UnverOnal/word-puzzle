using System;
using System.Collections.Generic;
using GameManagement;
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

        private GamePlayModel _gamePlayModel;

        private readonly ObjectPool<MoveCommand> _moveCommandPool;

        [Inject]
        public GamePlayPresenter(IInputService inputService, IPoolService poolService, ICommandService commandService,
            LevelPresenter levelPresenter, GameSettings gameSettings)
        {
            _inputService = inputService;
            _commandInvoker = commandService.GetCommandInvoker();
            _levelPresenter = levelPresenter;

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
            var targetPosition = _levelPresenter.FormingTiles[0].GameObject.transform.position;
            moveCommand.SetMoveData(tile, targetPosition);

            _commandInvoker.ExecuteCommand(moveCommand);
        }

        public void Undo()
        {
            _commandInvoker.UndoCommand();
        }

        public void UndoAll()
        {
            _commandInvoker.UndoCommandAll();
        }

        public void Submit()
        {
            Debug.Log("Submit");
            _commandInvoker.Reset();
        }

        private Tile GetSelectedTile(GameObject gameObject)
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