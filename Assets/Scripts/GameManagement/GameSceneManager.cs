using GamePlay;
using UI;
using VContainer;
using VContainer.Unity;

namespace GameManagement
{
    public class GameSceneManager : IInitializable
    {
        private readonly UiManager _uiManager;
        private readonly GamePlayPresenter _gamePlayPresenter;

        [Inject]
        public GameSceneManager(UiManager uiManager, GamePlayPresenter gamePlayPresenter)
        {
            _uiManager = uiManager;
            _gamePlayPresenter = gamePlayPresenter;
        }

        public void Initialize()
        {
            _uiManager.Initialize();
            _gamePlayPresenter.Initialize();
        }
    }
}
