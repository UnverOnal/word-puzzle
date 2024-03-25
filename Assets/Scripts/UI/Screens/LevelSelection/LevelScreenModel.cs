using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using LevelCreation;
using Services.DataStorageService;

namespace UI.Screens.LevelSelection
{
    public class LevelScreenModel
    {
        public List<LevelDisplayData> LevelDisplayDatas { get; private set; }

        private readonly List<LevelCreationData> _levelCreationDatas;
        private readonly IDataStorageService _dataStorageService;

        public LevelScreenModel(List<LevelCreationData> levelCreationDatas, IDataStorageService dataStorageService)
        {
            _levelCreationDatas = levelCreationDatas;
            _dataStorageService = dataStorageService;

            LevelDisplayDatas = new List<LevelDisplayData>();
        }

        private async Task<Dictionary<int, LevelData>> SetLevelStatusData()
        {
            var gameData = await _dataStorageService.GetFileContentAsync<GameData>();
            var levelStatusMap = gameData.levelStatusMap;
            return levelStatusMap;
        }

        public async UniTask SetLevelData()
        {
            var levelStatusMap = await SetLevelStatusData();

            LevelDisplayDatas.Clear();
            for (var i = 0; i < _levelCreationDatas.Count; i++)
            {
                levelStatusMap.TryGetValue(i, out var levelStatus);
                if(i == 0 && levelStatus.highScore == 0) levelStatus.SetDefault();

                var data = new LevelDisplayData
                {
                    levelCount = i + 1,
                    highScore = levelStatus.highScore,
                    title = _levelCreationDatas[i].title,
                    playStatus = levelStatus.playStatus
                };
                LevelDisplayDatas.Add(data);
            }
        }
    }
}