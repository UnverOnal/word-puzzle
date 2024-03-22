using System.Collections.Generic;

namespace LevelCreation
{
    public class LevelData
    {
        public string title;
        public List<Tile> tiles;
    }

    public class Tile
    {
        public int id;
        public Position position;
        public string character;
        public List<int> children;
    }

    public class Position
    {
        public float x;
        public float y;
        public float z;
    }
}