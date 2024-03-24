namespace UI.Screens.LevelSelection
{
    public enum PlayStatus
    {
        Locked,
        Played,
        Playable
    }
    
    public struct LevelDisplayData
    {
        public string title;
        public int highScore;
        public int levelCount;
        public PlayStatus playStatus;
    }
}
