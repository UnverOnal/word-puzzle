namespace UI.Screens.Game.LevelEnd
{
    public class LevelEndView
    {
        private readonly LevelEndResources _resources;

        public LevelEndView(LevelEndResources resources)
        {
            _resources = resources;
        }

        public void SetHighScore(int highScore)
        {
            _resources.highScore.text = "High Score : " + highScore;
        }
    }
}
