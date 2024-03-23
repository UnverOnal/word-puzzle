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
        private readonly DictionaryPreprocessor _dictionaryPreprocessor;

        [Inject]
        public GameSceneManager(UiManager uiManager, GamePlayPresenter gamePlayPresenter, DictionaryPreprocessor dictionaryPreprocessor)
        {
            _uiManager = uiManager;
            _gamePlayPresenter = gamePlayPresenter;
            _dictionaryPreprocessor = dictionaryPreprocessor;
        }

        public void Initialize()
        {
            _uiManager.Initialize();
            _gamePlayPresenter.Initialize();
        }
    }
}
