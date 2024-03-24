using System.Collections.Generic;
using UI.Screens.LevelSelection;

namespace Services.DataStorageService
{
    public class GameData : LocalSaveData
    {
        public Dictionary<int, LevelStatus> levelStatusMap = new();
    }

    public struct LevelStatus
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
