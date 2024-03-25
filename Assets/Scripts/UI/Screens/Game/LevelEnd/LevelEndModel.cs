using GamePlay.Score;

namespace UI.Screens.Game.LevelEnd
{
    public class LevelEndModel
    {
        public int Score => _scorePresenter.Score;
        public bool IsHighScore => _scorePresenter.Score > _scorePresenter.HighScore;

        private readonly ScorePresenter _scorePresenter;
        
        public LevelEndModel(ScorePresenter scorePresenter)
        {
            _scorePresenter = scorePresenter;
        }
    }
}