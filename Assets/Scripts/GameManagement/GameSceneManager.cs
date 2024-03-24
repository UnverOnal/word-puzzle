using Dictionary;
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
        private readonly WordDictionary _wordDictionary;

        [Inject]
        public GameSceneManager(UiManager uiManager, GamePlayPresenter gamePlayPresenter, WordDictionary wordDictionary)
        {
            _uiManager = uiManager;
            _gamePlayPresenter = gamePlayPresenter;
            _wordDictionary = wordDictionary;
        }

        public void Initialize()
        {
            _uiManager.Initialize();
            _gamePlayPresenter.Initialize();
        }
    }
}
