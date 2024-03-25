using System.Collections.Generic;
using System.Linq;
using GameManagement;
using GamePlay.TileSystem;
using UnityEngine;

namespace LevelCreation
{
    public class LevelFitter
    {
        public Vector3 BoundsCenter => _bounds.center;

        private readonly float _cameraSizeOffset;
        private readonly Vector3 _cameraPositionOffset;
        private readonly Camera _camera;
        private Bounds _bounds;

        public LevelFitter(GameSettings gameSettings)
        {
            _cameraSizeOffset = gameSettings.cameraSizeOffset;
            _cameraPositionOffset = gameSettings.cameraPositionOffset;
            _camera = Camera.main;
        }

        public void AlignCamera(IReadOnlyList<Tile> tiles)
        {
            // var tiles = levelCreationData.tiles;
            // var orderedTiles = tiles.OrderBy(data => data.position.x).ToList();
            UpdateCamera(tiles);
        }
        
        private void UpdateCamera(IReadOnlyList<Tile> tiles)
        {
            _bounds = CalculateBounds(tiles);

            var cameraPosition = CalculateCameraPosition(_bounds);
            var orthographicSize = CalculateOrthographicSize(_bounds);
            SetCamera(cameraPosition, orthographicSize);
        }
        
        private Bounds CalculateBounds(IReadOnlyList<Tile> tiles)
        {
            var minPosition = tiles[0].Position;
            var maxPosition = tiles[0].Position;

            for (int i = 0; i < tiles.Count; i++)
            {
                var tile = tiles[i];
                
                minPosition.x = Mathf.Min(minPosition.x, tile.Position.x);
                minPosition.y = Mathf.Min(minPosition.y, tile.Position.y);
                maxPosition.x = Mathf.Max(maxPosition.x, tile.Position.x);
                maxPosition.y = Mathf.Max(maxPosition.y, tile.Position.y);
            }

            var bounds = new Bounds
            {
                min = minPosition,
                max = maxPosition
            };
            return bounds;
        }
        
        private Vector2 CalculateCameraPosition(Bounds bounds)
        {
            Vector2 center = bounds.center;
            return center;
        }
        
        private float CalculateOrthographicSize(Bounds bounds)
        {
            var screenAspect = (float)Screen.width / Screen.height;

            var boundsWidth = bounds.size.x;
            var boundsHeight = bounds.size.y;

            var orthographicSize = Mathf.Max(boundsWidth / 2, boundsHeight / 2);

            orthographicSize /= screenAspect;

            return orthographicSize;
        }
        
        private void SetCamera(Vector2 position, float orthographicSize)
        {
            var cameraTransform = _camera.transform;
            cameraTransform.position = new Vector3(position.x, position.y, cameraTransform.position.z) + _cameraPositionOffset;

            _camera.orthographicSize = orthographicSize + _cameraSizeOffset;
        }
    }
}
