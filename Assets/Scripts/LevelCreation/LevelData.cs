using System.Collections.Generic;

namespace LevelCreation
{
    public class LevelData
    {
        public string title;
        public List<TileData> tiles;
    }

    public class TileData
    {
        public int id;
        public TilePositionData position;
        public string character;
        public List<int> children;
    }

    public class TilePositionData
    {
        public float x;
        public float y;
        public float z;
    }
}