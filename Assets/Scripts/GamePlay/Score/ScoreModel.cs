using LevelCreation;
using Services.DataStorageService;

namespace GamePlay.Score
{
    public class ScoreModel
    { 
        public int Score { get; private set; }
        public int HighScore { get; private set; }
        
        private readonly IDataStorageService _dataStorageService;
        private readonly LevelPresenter _levelPresenter;

        public ScoreModel(IDataStorageService dataStorageService, LevelPresenter levelPresenter)
        {
            _dataStorageService = dataStorageService;
            _levelPresenter = levelPresenter;

            GetHighScore();
        }

        public void UpdateScore(int score)
        {
            Score = score;
        }

        public void AddPoint(int point)
        {
            Score += point;
        }

        public void Reset()
        {
            GetHighScore();
            Score = 0;
        }
        
        private async void GetHighScore()
        {
            var data = await _dataStorageService.GetFileContentAsync<GameData>();

            var levelIndex = _levelPresenter.CurrentLevelIndex;
            var exist = data.levelStatusMap.TryGetValue(levelIndex, out var levelStatus);
            if(!exist) levelStatus.SetDefault();
            HighScore =levelStatus.highScore;
        }
    }
}
