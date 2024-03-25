using System.Collections.Generic;
using GamePlay.TileSystem;
using LevelCreation;
using UnityEngine;

namespace GamePlay
{
    public class GamePlayModel
    {
        public List<LetterTile> WrongTiles { get; }
        public List<LetterTile> Tiles { get; }
        
        public GamePlayModel(LevelPresenter levelPresenter)
        {
            Tiles = levelPresenter.Tiles;
            WrongTiles = new List<LetterTile>();
        }

        public void AddTile(LetterTile tile)
        {
            Tiles.Add(tile);
        }

        public void AddTiles(List<LetterTile> letterTiles)
        {
            Tiles.AddRange(letterTiles);
        }

        public void RemoveTile(LetterTile tile)
        {
            Tiles.Remove(tile);
        }

        public void AddWrongTiles(IEnumerable<LetterTile> tiles)
        {
            WrongTiles.Clear();
            WrongTiles.AddRange(tiles);
        }

        public void Reset()
        {
            Tiles.Clear();
            WrongTiles.Clear();
        }
    }
}