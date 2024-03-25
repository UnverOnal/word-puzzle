using System.Collections.Generic;
using GameManagement;
using GamePlay.TileSystem;
using UnityEngine;

namespace LevelCreation
{
    public class LevelView
    {
        private readonly GameSettings _gameSettings;

        public LevelView(GameSettings gameSettings)
        {
            _gameSettings = gameSettings;
        }

        public void SetTile(LetterTile letterTile) => letterTile.Initialize();

        public void SetFormingArea(Vector3 initialPosition, List<BlankTile> blankTiles)
        {
            float? nextTileDistance = null;
            for (var i = 0; i < _gameSettings.formingAreaSize; i++)
            {
                var blankTile = blankTiles[i];
                nextTileDistance ??= CalculateFormingTileDistance(blankTile);

                var position = CalculateFormingTilePosition(i, initialPosition, nextTileDistance.Value);
                blankTile.Initialize();
                blankTile.SetPosition(position);
            }
        }

        private Vector3 CalculateFormingTilePosition(int index, Vector3 initialPosition, float distance)
        {
            var position = initialPosition +
                           Vector3.right * (_gameSettings.formingAreaSize - 1) * distance / -2f +
                           Vector3.right * distance * index +
                           _gameSettings.formingAreaOffset;

            return position;
        }

        private float CalculateFormingTileDistance(BlankTile tile)
        {
            var distance = tile.Sprite.bounds.size.x * tile.GameObject.transform.localScale.x;
            return distance;
        }
    }
}