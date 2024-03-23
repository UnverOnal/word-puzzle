using System.Collections.Generic;
using System.Linq;
using GameManagement;
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

        public void AlignCamera(LevelData levelData)
        {
            var tiles = levelData.tiles;
            var orderedTiles = tiles.OrderBy(data => data.position.x).ToList();
            UpdateCamera(orderedTiles);
        }
        
        private void UpdateCamera(IReadOnlyList<TileData> tiles)
        {
            _bounds = CalculateBounds(tiles);

            var cameraPosition = CalculateCameraPosition(_bounds);
            var orthographicSize = CalculateOrthographicSize(_bounds);
            SetCamera(cameraPosition, orthographicSize);
        }
        
        private Bounds CalculateBounds(IReadOnlyList<TileData> tiles)
        {
            var minPosition = new Vector2(tiles[0].position.x, tiles[0].position.y);
            var maxPosition = new Vector2(tiles[0].position.x, tiles[0].position.y);

            for (int i = 0; i < tiles.Count; i++)
            {
                var tile = tiles[i];
                
                minPosition.x = Mathf.Min(minPosition.x, tile.position.x);
                minPosition.y = Mathf.Min(minPosition.y, tile.position.y);
                maxPosition.x = Mathf.Max(maxPosition.x, tile.position.x);
                maxPosition.y = Mathf.Max(maxPosition.y, tile.position.y);
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
