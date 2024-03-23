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
        private readonly GameStatePresenter _statePresenter;

        [Inject]
        public GameSceneManager(UiManager uiManager,GameStatePresenter statePresenter)
        {
            _uiManager = uiManager;
            _statePresenter = statePresenter;
        }

        public void Initialize()
        {
            _uiManager.Initialize();
            _statePresenter.UpdateGameState(GameState.GameState.Home);
        }
    }
}
