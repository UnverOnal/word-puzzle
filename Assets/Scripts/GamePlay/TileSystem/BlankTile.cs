using UnityEngine;

namespace GamePlay.TileSystem
{
    public class EmptyTile
    {
        public int ID { get; set; }
        public Sprite Sprite { get; }
        public GameObject GameObject { get; }
        
        private readonly GameObject _tilePrefab;

        public EmptyTile(GameObject tilePrefab, Transform tileParent)
        {
            _tilePrefab = tilePrefab;
            GameObject = Object.Instantiate(tilePrefab, tileParent);
            Sprite = GameObject.GetComponent<SpriteRenderer>().sprite;
        }

        public void SetPosition(Vector3 position)
        {
            GameObject.transform.position = position;
        }
    }
}
