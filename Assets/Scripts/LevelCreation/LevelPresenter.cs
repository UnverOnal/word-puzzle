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
        private readonly GameSettings _gameSettings;
        public int CurrentLevelIndex { get; private set; }
        public List<LevelCreationData> LevelDatas => _levelModel.LevelCreationDatas;
        public LevelCreationData LevelCreationData { get; private set; }

        public List<LetterTile> Tiles => _levelModel.LetterTiles;
        public List<BlankTile> FormingTiles => _levelModel.FormingTiles;

        private ObjectPool<LetterTile> _letterTilePool;
        private ObjectPool<BlankTile> _blankTilePool;

        private readonly LevelModel _levelModel;
        private readonly LevelView _levelView;
        private readonly LevelFitter _levelFitter;

        [Inject]
        public LevelPresenter(GameSettings gameSettings, LevelAssets levelAssets,
            IFileConversionService fileConversionService,
            IPoolService poolService)
        {
            _gameSettings = gameSettings;
            _levelModel = new LevelModel(fileConversionService, levelAssets);
            _levelView = new LevelView(gameSettings, levelAssets);
            _levelFitter = new LevelFitter(gameSettings);

            _levelModel.CreateLevelCreationData();

            CreateTilePools(poolService, levelAssets, gameSettings);
        }

        public void CreateLevel(int levelIndex)
        {
            CurrentLevelIndex = levelIndex;
            LevelCreationData = _levelModel.GetLevel(levelIndex);

            for (var i = 0; i < LevelCreationData.tiles.Count; i++)
            {
                var tileData = LevelCreationData.tiles[i];

                var tile = _letterTilePool.Get();
                tile.SetTileData(tileData);
                _levelView.SetTile(tile);
                
                _levelModel.LetterTiles.Add(tile);
            }

            SetChildrenTiles();
            _levelFitter.AlignCamera(_levelModel.LetterTiles);
            CreateFormingArea();
        }

        public void ReturnTile(Tile tile)
        {
            tile.Reset();

            var tileType = tile.GetType();
            if(tileType == typeof(LetterTile))
                _letterTilePool.Return((LetterTile)tile);
            else
                _blankTilePool.Return((BlankTile)tile);
        }
        
        public void ReturnTile(IEnumerable<Tile> tiles)
        {
            foreach (var tile in tiles)
                ReturnTile(tile);
        }

        private void CreateFormingArea()
        {
            var formingTiles = new List<BlankTile>();
            for (int i = 0; i < _gameSettings.formingAreaSize; i++)
            {
                var blankTile = _blankTilePool.Get();
                blankTile.Initialize();
                formingTiles.Add(blankTile);
            }
            _levelView.SetFormingArea(_levelFitter.BoundsCenter, formingTiles);
            _levelModel.FormingTiles = formingTiles;
        }

        private void SetChildrenTiles()
        {
            var tiles = _levelModel.LetterTiles;
            for (var i = 0; i < tiles.Count; i++)
                tiles[i].AddChildren(tiles);
        }

        private void CreateTilePools(IPoolService poolService, LevelAssets levelAssets, GameSettings gameSettings)
        {
            var letterTileParent = new GameObject("LetterTileParent").transform;
            _letterTilePool = poolService.GetPoolFactory()
                .CreatePool(() => new LetterTile(levelAssets.tilePrefab, letterTileParent));

            var blankTileParent = new GameObject("FormingArea").transform;
            _blankTilePool = poolService.GetPoolFactory()
                .CreatePool(() => new BlankTile(levelAssets.emptyTilePrefab, blankTileParent),
                    false, gameSettings.formingAreaSize);
        }
    }
}