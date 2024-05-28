using System.Collections.Generic;
using UI.Screens.Home.Level;

namespace Services.DataStorageService
{
    public class GameData : LocalSaveData
    {
        public Dictionary<int, LevelData> levelStatusMap = new();
    }

    public struct LevelData
    {
        public int highScore;
        public PlayStatus playStatus;

        public void SetDefault()
        {
            highScore = 0;
            playStatus = PlayStatus.Playable;
        }
    }
}
