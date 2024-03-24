using LevelCreation;

namespace UI.Screens.Game
{
    public class GameScreenView : ScreenView
    {
        private readonly LevelPresenter _levelPresenter;
        private readonly GameScreenResources _resources;
        
        public GameScreenView(ScreenResources screenResources, LevelPresenter levelPresenter) : base(screenResources)
        {
            _levelPresenter = levelPresenter;
            _resources = (GameScreenResources)screenResources;
        }

        public void SetScore(int score)
        {
            var scoreText = "Score : " + score;
            _resources.inGameScore.text = scoreText;
        }

        public void SetLevelTitle()
        {
            var title = _levelPresenter.LevelCreationData?.title;
            _resources.levelTitleText.text = title;
        }
    }
}
