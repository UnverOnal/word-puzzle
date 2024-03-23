using UnityEngine;

namespace GamePlay.TileSystem
{
    public class Tile
    {
        public virtual int ID { get; set; }
        public GameObject GameObject { get; protected set; }
    }
}
