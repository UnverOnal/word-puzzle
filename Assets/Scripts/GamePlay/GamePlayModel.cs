namespace GamePlay
{
    public class GamePlayModel
    {
        public int FormingAreaIndex { get; private set; }

        public void IncreaseFormingAreaIndex()
        {
            FormingAreaIndex++;
        }

        public void DecreaseFormingAreaIndex()
        {
            FormingAreaIndex--;
        }

        public void ResetFormingAreaIndex()
        {
            FormingAreaIndex = 0;
        }
    }
}
