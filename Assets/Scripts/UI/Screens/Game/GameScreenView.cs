namespace UI.Screens.Game
{
    public class GameScreenView : ScreenView
    {
        private readonly GameScreenResources _resources;
        
        public GameScreenView(ScreenResources screenResources) : base(screenResources)
        {
            _resources = (GameScreenResources)screenResources;
        }

        public void SetScore(int score)
        {
            var scoreText = "Score : " + score;
            _resources.inGameScore.text = scoreText;
        }
    }
}
