using GameState;
using UI.Screens.Game.LevelEnd;
using VContainer;

namespace UI.Screens.Game
{
    public class GameScreenPresenter : ScreenPresenter, IScreenPresenter
    {
        private readonly GameScreenView _screenView;
        private readonly LevelEndPresenter _levelEndPresenter;
        
        [Inject]
        public GameScreenPresenter(GameScreenResources resources, GameStatePresenter statePresenter) : base(statePresenter)
        {
            _screenView = new GameScreenView(resources);
            _levelEndPresenter = new LevelEndPresenter(resources.levelEndResources);
        }

        public void Initialize()
        {
            SetStateAction();
        }

        public void UpdateScore(int score)
        {
            _screenView.SetScore(score);
        }

        public void InitializeLevelEnd()
        {
            _levelEndPresenter.Initialize();
        }

        public void UpdateLevelTitle(string title)
        {
            _screenView.SetLevelTitle(title);
        }

        protected override void OnStateUpdate(GameState.GameState gameState)
        {
            if(gameState == GameState.GameState.Game)
                _screenView.Enable();
            else if(_screenView.IsActive)
                _screenView.Disable();
        }
    }
}
