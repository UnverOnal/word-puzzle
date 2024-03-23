using System;

namespace GameState
{
    public class GameStatePresenter
    {
        public GameState CurrentState => _gameStateModel.GameState;
        public event Action<GameState> OnStateUpdate;

        private readonly GameStateModel _gameStateModel;
        
        public GameStatePresenter()
        {
            _gameStateModel = new GameStateModel();
            UpdateGameState(GameState.Home);
        }

        public void UpdateGameState(GameState state)
        {
            _gameStateModel.SetGameState(state);
            OnStateUpdate?.Invoke(state);
        }
    }
}
