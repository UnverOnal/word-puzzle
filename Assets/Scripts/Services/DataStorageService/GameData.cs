using System.Collections.Generic;

namespace Services.DataStorageService
{
    public class GameData : LocalSaveData
    {
        public Dictionary<int, int> levelHighScore = new();
    }
}
