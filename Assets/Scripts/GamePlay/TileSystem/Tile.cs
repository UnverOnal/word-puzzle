using System.Collections.Generic;
using LevelCreation;
using TMPro;
using UnityEngine;

namespace GamePlay.TileSystem
{
    public class Tile
    {
        public int ID => _tileData.id;
        
        private readonly List<Tile> _childrenTiles = new();
        private readonly List<Tile> _parentTiles = new();

        private TileData _tileData;

        private readonly GameObject _gameObject;
        private readonly SpriteRenderer _spriteRenderer;
        private readonly TextMeshProUGUI _charText;

        public Tile(GameObject tilePrefab, Transform poolTransform)
        {
            _gameObject = Object.Instantiate(tilePrefab, poolTransform);
            _charText = _gameObject.GetComponentInChildren<TextMeshProUGUI>();
            _spriteRenderer = _gameObject.GetComponent<SpriteRenderer>();
        }

        public void SetTileData(TileData tileData)
        {
            _tileData = tileData;
        }

        public void SetPosition()
        {
            var position = _tileData.position;
            _gameObject.transform.position = new Vector3(position.x, position.y, position.z);
        }

        public void SetChar()
        {
            _charText.text = _tileData.character;
        }
        
        public void AddChildren(List<Tile> tiles)
        {
            var childrenIndices = _tileData.children;
            for (int i = 0; i < tiles.Count; i++)
            {
                if(childrenIndices.Count == 0) break;
                
                var tile = tiles[i];
                if(tile == this)
                    continue;

                if (childrenIndices.Contains(tile.ID))
                {
                    AddChild(tile);
                    childrenIndices.Remove(tile.ID);
                }
            }
        }

        public void AddChild(Tile tile)
        {
            _childrenTiles.Add(tile);
            tile.AddParent(this);
        }

        public void RemoveChild(Tile tile)
        {
            _childrenTiles.Remove(tile);
            tile.RemoveParent(this);
        }

        private void AddParent(Tile tile)
        {
            _parentTiles.Add(tile);
            
            if(_parentTiles.Count == 1)
                MakeInteractable(false);
        }

        private void RemoveParent(Tile tile)
        {
            _parentTiles.Remove(tile);
            
            if(_parentTiles.Count < 1)
                MakeInteractable(true);
        }

        public void Reset()
        {
            _charText.text = string.Empty;
            _gameObject.transform.position = Vector3.zero;
            _childrenTiles.Clear();
            _tileData = null;
        }

        private void MakeInteractable(bool canInteract)
        {
            var color = canInteract ? Color.white : (Color.gray + Color.white)/2f;
            _spriteRenderer.color = color;
            var layer = canInteract ? LayerMask.NameToLayer("Tile") : LayerMask.NameToLayer("Default");
            _gameObject.layer = layer;
        }
    }
}