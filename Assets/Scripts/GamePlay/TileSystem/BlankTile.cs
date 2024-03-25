using UnityEngine;

namespace GamePlay.TileSystem
{
    public class BlankTile : Tile
    {
        public Sprite Sprite { get; }
        
        public BlankTile(GameObject tilePrefab, Transform tileParent)
        {
            GameObject = Object.Instantiate(tilePrefab, tileParent);
            Sprite = GameObject.GetComponent<SpriteRenderer>().sprite;
        }

        public override void Initialize()
        {
            GameObject.SetActive(true);
            GameObject.transform.position = Vector3.zero;
        }
        
        public void SetPosition(Vector3 position) => GameObject.transform.position = position;

        public override void Reset() => GameObject.SetActive(false);
    }
}
