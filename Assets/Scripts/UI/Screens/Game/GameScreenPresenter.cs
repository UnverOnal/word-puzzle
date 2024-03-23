using GamePlay;
using GameState;
using LevelCreation;
using UI.Screens.Game.LevelEnd;
using VContainer;

namespace UI.Screens.Game
{
    public class GameScreenPresenter : ScreenPresenter, IScreenPresenter
    {
        private readonly GameScreenResources _resources;
        private readonly GamePlayPresenter _gamePlayPresenter;
        private readonly GameScreenView _screenView;
        private readonly LevelEndPresenter _levelEndPresenter;

        [Inject]
        public GameScreenPresenter(GameScreenResources resources, GameStatePresenter statePresenter,
            GamePlayPresenter gamePlayPresenter, LevelPresenter levelPresenter) : base(statePresenter)
        {
            _resources = resources;
            _gamePlayPresenter = gamePlayPresenter;
            _screenView = new GameScreenView(resources, levelPresenter);
            _levelEndPresenter = new LevelEndPresenter(resources.levelEndResources);
        }

        public void Initialize()
        {
            SetStateAction();
            _screenView.SetLevelTitle();
            SubscribeButtons();
        }

        public void UpdateScore(int score)
        {
            _screenView.SetScore(score);
        }

        public void InitializeLevelEnd()
        {
            _levelEndPresenter.Initialize();
        }

        protected override void OnStateUpdate(GameState.GameState gameState)
        {
            if (gameState == GameState.GameState.Game)
                _screenView.Enable();
            else if (_screenView.IsActive)
                _screenView.Disable();
        }

        private void SubscribeButtons()
        {
            _resources.undoButton.OnClick += _gamePlayPresenter.Undo;
            _resources.undoButton.OnHold += _gamePlayPresenter.UndoAll;
            _resources.submitButton.onClick.AddListener(_gamePlayPresenter.Submit);
        }
        
        private void UnsubscribeButtons()
        {
            _resources.undoButton.OnClick -= _gamePlayPresenter.Undo;
            _resources.undoButton.OnHold -= _gamePlayPresenter.UndoAll;
            _resources.submitButton.onClick.RemoveListener(_gamePlayPresenter.Submit);
        }

        public override void Dispose()
        {
            base.Dispose();
            UnsubscribeButtons();
        }
    }
}