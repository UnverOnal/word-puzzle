using UnityEngine;

namespace GamePlay.TileSystem
{
    public abstract class Tile
    {
        public virtual int ID { get; set; }
        public GameObject GameObject { get; protected set; }

        public abstract void Initialize();

        public abstract void Reset();
    }
}
