using UnityEngine;

namespace GamePlay.TileSystem
{
    public class BlankTile : Tile
    {
        public Sprite Sprite { get; }

        private LetterTile _letterTile; 
        
        public BlankTile(GameObject tilePrefab, Transform tileParent)
        {
            GameObject = Object.Instantiate(tilePrefab, tileParent);
            Sprite = GameObject.GetComponent<SpriteRenderer>().sprite;
        }

        public void SetLetterTile(LetterTile letterTile)
        {
            _letterTile = letterTile;
        }

        public void SetPosition(Vector3 position)
        {
            GameObject.transform.position = position;
        }
    }
}
