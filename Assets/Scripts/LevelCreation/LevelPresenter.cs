using System.Collections.Generic;
using GamePlay.TileSystem;
using Services.FileConversionService;
using Services.PoolingService;
using UI.Screens.Game;
using UnityEngine;
using VContainer;

namespace LevelCreation
{
    public class LevelPresenter
    {
        private readonly LevelAssets _levelAssets;
        private readonly GameScreenPresenter _gameScreenPresenter;

        private readonly ObjectPool<Tile> _tilePool;

        private readonly LevelModel _levelModel;
        private readonly LevelView _levelView;
        private readonly LevelFitter _levelFitter;


        [Inject]
        public LevelPresenter(GameSettings gameSettings, LevelAssets levelAssets,
            IFileConversionService fileConversionService, GameScreenPresenter gameScreenPresenter,
            IPoolService poolService)
        {
            _levelAssets = levelAssets;
            _gameScreenPresenter = gameScreenPresenter;

            _levelModel = new LevelModel(fileConversionService, levelAssets);
            _levelView = new LevelView(gameScreenPresenter, gameSettings, levelAssets);
            _levelFitter = new LevelFitter(gameSettings);

            _levelModel.CreateLevelData();

            var tileParent = new GameObject("TileParent");
            _tilePool = poolService.GetPoolFactory()
                .CreatePool(() => new Tile(levelAssets.tilePrefab, tileParent.transform));
        }

        public void CreateLevel()
        {
            var levelData = _levelModel.CurrentLevel;
            _levelFitter.AlignCamera(levelData);

            _levelView.SetLevelTitle(levelData.title);

            for (var i = 0; i < levelData.tiles.Count; i++)
            {
                var tileData = levelData.tiles[i];
                
                var tile = _tilePool.Get();
                tile.SetTileData(tileData);
                _levelModel.Tiles.Add(tile);
                
                _levelView.SetTile(tile);
            }
            
            SetChildrenTiles();
            CreateFormingArea();
        }

        private void CreateFormingArea()
        {
            var formingTiles = _levelView.SetFormingArea(_levelFitter.BoundsCenter);
            _levelModel.FormingTiles = formingTiles;
        }

        private void SetChildrenTiles()
        {
            var tiles = _levelModel.Tiles;
            for (int i = 0; i < tiles.Count; i++)
                tiles[i].AddChildren(tiles);
        }
    }
}