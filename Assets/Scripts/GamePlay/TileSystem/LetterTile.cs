using System.Collections.Generic;
using LevelCreation;
using Services.DataStorageService;
using TMPro;
using UnityEngine;

namespace GamePlay.TileSystem
{
    public class LetterTile : Tile
    {
        public override int ID => _tileData.id;

        public string Character { get; private set; }
        
        private readonly List<LetterTile> _childrenTiles = new();
        private readonly List<LetterTile> _parentTiles = new();

        private TileData _tileData;

        private readonly SpriteRenderer _spriteRenderer;
        private readonly TextMeshProUGUI _charText;

        public LetterTile(GameObject tilePrefab, Transform poolTransform)
        {
            GameObject = Object.Instantiate(tilePrefab, poolTransform);
            _charText = GameObject.GetComponentInChildren<TextMeshProUGUI>();
            _spriteRenderer = GameObject.GetComponent<SpriteRenderer>();
        }

        public void SetTileData(TileData tileData)
        {
            _tileData = tileData;
        }
        
        public override void Initialize()
        {
            GameObject.SetActive(true);
            SetPosition();
            SetChar();
        }
        
        public override void Reset()
        {
            _charText.text = string.Empty;
            GameObject.transform.position = Vector3.zero;
            GameObject.SetActive(false);
            _childrenTiles.Clear();
            _tileData = null;
        }

        public void AddChildren(List<LetterTile> allTiles)
        {
            var childrenIndices = new List<int>(_tileData.children);
            for (int i = 0; i < allTiles.Count; i++)
            {
                if(childrenIndices.Count == 0) break;
                
                var tile = allTiles[i];
                if(tile == this)
                    continue;

                if (childrenIndices.Contains(tile.ID))
                {
                    AddChild(tile);
                    childrenIndices.Remove(tile.ID);
                }
            }
        }

        public void RemoveChildren(out List<LetterTile> childrenToBeRemoved)
        {
            childrenToBeRemoved = new List<LetterTile>(_childrenTiles);
            while (_childrenTiles.Count > 0)
            {
                var tile = _childrenTiles[^1];
                RemoveChild(tile);
            }
        }

        public void AddChild(LetterTile letterTile)
        {
            _childrenTiles.Add(letterTile);
            letterTile.AddParent(this);
        }

        public void RemoveChild(LetterTile letterTile)
        {
            _childrenTiles.Remove(letterTile);
            letterTile.RemoveParent(this);
        }

        private void AddParent(LetterTile letterTile)
        {
            _parentTiles.Add(letterTile);
            
            if(_parentTiles.Count > 0)
                MakeInteractable(false);
        }

        private void RemoveParent(LetterTile letterTile)
        {
            _parentTiles.Remove(letterTile);
            
            if(_parentTiles.Count < 1)
                MakeInteractable(true);
        }

        public bool IsSame(GameObject gameObject) => gameObject == GameObject;
        
        private void SetPosition()
        {
            var position = _tileData.position;
            GameObject.transform.position = new Vector3(position.x, position.y, position.z);
        }

        private void SetChar()
        {
            Character = _tileData.character;
            _charText.text = _tileData.character;
        }

        private void MakeInteractable(bool canInteract)
        {
            var color = canInteract ? Color.white : (Color.gray + Color.white)/2f;
            _spriteRenderer.color = color;
            var layer = canInteract ? LayerMask.NameToLayer("Tile") : LayerMask.NameToLayer("Default");
            GameObject.layer = layer;
        }
    }
}