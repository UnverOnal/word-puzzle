using GamePlay.Score;
using GameState;

namespace UI.Screens.Game.LevelEnd
{
    public class LevelEndPresenter
    {
        private readonly LevelEndResources _resources;
        private readonly GameStatePresenter _statePresenter;
        private readonly LevelEndView _levelEndView;
        private readonly LevelEndModel _levelEndModel;

        public LevelEndPresenter(LevelEndResources resources, GameStatePresenter statePresenter, ScorePresenter scorePresenter)
        {
            _resources = resources;
            _statePresenter = statePresenter;
            _levelEndView = new LevelEndView(resources);
            _levelEndModel = new LevelEndModel(scorePresenter);
        }

        public void Initialize()
        {
            _resources.levelEndGameObject.SetActive(true);
            _levelEndView.SetScore(_levelEndModel.Score, _levelEndModel.IsHighScore);
            _resources.tapToContinueButton.onClick.AddListener(()=>_statePresenter.UpdateGameState(GameState.GameState.Home));
        }

        public void Close() => _resources.levelEndGameObject.SetActive(false);
    }
}