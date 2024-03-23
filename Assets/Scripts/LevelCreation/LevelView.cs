using System.Collections.Generic;
using GameManagement;
using GamePlay.TileSystem;
using UnityEngine;

namespace LevelCreation
{
    public class LevelView
    {
        private readonly GameSettings _gameSettings;
        private readonly LevelAssets _levelAssets;
        private readonly Transform _emptyTileParent;

        public LevelView(GameSettings gameSettings, LevelAssets levelAssets)
        {
            _gameSettings = gameSettings;
            _levelAssets = levelAssets;

            _emptyTileParent = new GameObject("FormingArea").transform;
        }

        public void SetTile(LetterTile letterTile)
        {
            letterTile.SetPosition();
            letterTile.SetChar();
        }

        public List<BlankTile> SetFormingArea(Vector3 initialPosition)
        {
            var formingArea = new List<BlankTile>();

            float? nextTileDistance = null;
            for (var i = 0; i < _gameSettings.formingAreaSize; i++)
            {
                var emptyTile = new BlankTile(_levelAssets.emptyTilePrefab, _emptyTileParent.transform);
                nextTileDistance ??= CalculateFormingTileDistance(emptyTile);

                var position = CalculateFormingTilePosition(i, initialPosition, nextTileDistance.Value);
                emptyTile.SetPosition(position);

                formingArea.Add(emptyTile);
            }

            return formingArea;
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