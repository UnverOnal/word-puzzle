using System.Collections.Generic;
using GamePlay.TileSystem;
using UI.Screens.Game;
using UnityEngine;

namespace LevelCreation
{
    public class LevelView 
    {
        private readonly GameScreenPresenter _gameScreenPresenter;
        private readonly GameSettings _gameSettings;
        private readonly LevelAssets _levelAssets;
        private readonly Transform _emptyTileParent;

        public LevelView(GameScreenPresenter gameScreenPresenter, GameSettings gameSettings, LevelAssets levelAssets)
        {
            _gameScreenPresenter = gameScreenPresenter;
            _gameSettings = gameSettings;
            _levelAssets = levelAssets;

            _emptyTileParent = new GameObject("FormingArea").transform;
        }

        public void SetLevelTitle(string title)
        {
            _gameScreenPresenter.UpdateLevelTitle(title);
        }

        public void SetTile(Tile tile)
        {
            tile.SetPosition();
            tile.SetChar();
        }

        public List<EmptyTile> SetFormingArea(Vector3 initialPosition)
        {
            var formingArea = new List<EmptyTile>();
            
            float? nextTileDistance = null;
            for (int i = 0; i < _gameSettings.formingAreaSize; i++)
            {
                var emptyTile = new EmptyTile(_levelAssets.emptyTilePrefab, _emptyTileParent.transform);
                nextTileDistance ??= CalculateFormingTileDistance(emptyTile);

                var position = CalculateFormingTilePosition(i, initialPosition, nextTileDistance.Value);
                emptyTile.SetPosition(position);
            }

            return formingArea;
        }

        private Vector3 CalculateFormingTilePosition(int index, Vector3 initialPosition, float distance)
        {
            var position = initialPosition + 
                           Vector3.right * (_gameSettings.formingAreaSize - 1) * distance / -2f+
                           Vector3.right * distance * index +
                           _gameSettings.formingAreaOffset;

            return position;
        }
        
        private float CalculateFormingTileDistance(EmptyTile tile)
        {
            var distance = tile.Sprite.bounds.size.x * tile.GameObject.transform.localScale.x;
            return distance;
        }
    }
}
