namespace GamePlay.Score
{
    public class ScoreModel
    {
        public int Score { get; private set; }

        public void UpdateScore(int score)
        {
            Score = score;
        }
    }
}
