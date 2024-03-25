namespace UI.Screens.Game.LevelEnd
{
    public class LevelEndView
    {
        private readonly LevelEndResources _resources;

        public LevelEndView(LevelEndResources resources)
        {
            _resources = resources;
        }

        public void SetScore(int score, bool isHighScore)
        {
            var scoreText = isHighScore ? "High Score : " + score : "Score : " + score;
            _resources.highScore.text = scoreText;
        }
    }
}
