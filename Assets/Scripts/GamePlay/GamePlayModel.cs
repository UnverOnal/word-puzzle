using System.Collections.Generic;
using GamePlay.TileSystem;
using LevelCreation;
using UnityEngine;

namespace GamePlay
{
    public class GamePlayModel
    {
        public List<LetterTile> Tiles { get; }
        
        public GamePlayModel(LevelPresenter levelPresenter)
        {
            Tiles = levelPresenter.Tiles;
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

        public void Reset()
        {
            Tiles.Clear();
        }
    }
}