namespace GameState
{
    public enum GameState
    {
        Home,
        Game,
        LevelSelection,
        LevelEnd
    }
    
    public class GameStateModel
    {
        public GameState GameState { get; private set; }

        public void SetGameState(GameState state)
        {
            GameState = state;
        }
    }
}
