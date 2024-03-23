using GameState;
using LevelCreation;
using UI;
using VContainer;
using VContainer.Unity;

namespace GameManagement
{
    public class GameSceneManager : IInitializable
    {
        private readonly UiManager _uiManager;
        private readonly GameStatePresenter _gameStatePresenter;

        [Inject]
        public GameSceneManager(UiManager uiManager,GameStatePresenter gameStatePresenter)
        {
            _uiManager = uiManager;
            _gameStatePresenter = gameStatePresenter;
        }

        public void Initialize()
        {
            _uiManager.Initialize();
            _gameStatePresenter.UpdateGameState(GameState.GameState.Home);
        }
    }
}
