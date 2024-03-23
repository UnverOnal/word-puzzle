using System.Collections.Generic;
using GameManagement;
using GamePlay.TileSystem;
using Services.FileConversionService;
using Services.PoolingService;
using UnityEngine;
using VContainer;

namespace LevelCreation
{
    public class LevelPresenter
    {
        public List<LevelData> LevelDatas => _levelModel.LevelDatas;
        public LevelData CurrentLevelData => _levelModel.CurrentLevel;

        public List<LetterTile> Tiles => _levelModel.Tiles;
        public List<BlankTile> FormingTiles => _levelModel.FormingTiles;

        private readonly ObjectPool<LetterTile> _tilePool;

        private readonly LevelModel _levelModel;
        private readonly LevelView _levelView;
        private readonly LevelFitter _levelFitter;

        [Inject]
        public LevelPresenter(GameSettings gameSettings, LevelAssets levelAssets,
            IFileConversionService fileConversionService,
            IPoolService poolService)
        {
            _levelModel = new LevelModel(fileConversionService, levelAssets);
            _levelView = new LevelView(gameSettings, levelAssets);
            _levelFitter = new LevelFitter(gameSettings);

            _levelModel.CreateLevelData();

            var tileParent = new GameObject("TileParent");
            _tilePool = poolService.GetPoolFactory()
                .CreatePool(() => new LetterTile(levelAssets.tilePrefab, tileParent.transform));
        }

        public void CreateLevel()
        {
            var levelData = _levelModel.CurrentLevel;
            _levelFitter.AlignCamera(levelData);

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
            for (var i = 0; i < tiles.Count; i++)
                tiles[i].AddChildren(tiles);
        }
    }
}