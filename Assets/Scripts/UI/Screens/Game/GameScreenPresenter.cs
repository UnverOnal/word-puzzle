using GamePlay;
using GamePlay.Score;
using GameState;
using LevelCreation;
using UI.Screens.Game.LevelEnd;
using VContainer;

namespace UI.Screens.Game
{
    public class GameScreenPresenter : ScreenPresenter, IScreenPresenter
    {
        [Inject] private readonly GamePlayPresenter _gamePlayPresenter;

        private readonly GameScreenView _screenView;
        
        private readonly GameScreenResources _resources;
        private readonly ScorePresenter _scorePresenter;
        private readonly LevelEndPresenter _levelEndPresenter;

        [Inject]
        public GameScreenPresenter(GameScreenResources resources, GameStatePresenter statePresenter,
            LevelPresenter levelPresenter, ScorePresenter scorePresenter) : base(statePresenter)
        {
            _resources = resources;
            _scorePresenter = scorePresenter;
            _screenView = new GameScreenView(resources, levelPresenter);
            _levelEndPresenter = new LevelEndPresenter(resources.levelEndResources, statePresenter, scorePresenter);
        }

        public void Initialize()
        {
            UpdateButtonStatus(false);
            SetStateAction();
            SubscribeButtons();
            _scorePresenter.OnScoreUpdated += _screenView.SetScore;
            _gamePlayPresenter.OnMoveCommand += UpdateButtonStatus;
        }

        protected override void OnStateUpdate(GameState.GameState gameState)
        {
            switch (gameState)
            {
                case GameState.GameState.Game:
                    OnGameStart();
                    break;
                case GameState.GameState.LevelEnd:
                    OnLevelEndStart();
                    break;
                default:
                    _screenView.Disable();
                    _levelEndPresenter.Close();
                    break;
            }
        }
        
        //Update undo and submit buttons
        private void UpdateButtonStatus(bool active)
        {
            _resources.undoButton.SetInteractable(active);
            _resources.submitButton.interactable = active;
        }

        private void OnGameStart()
        {
            _scorePresenter.Reset();
            _screenView.SetLevelTitle();
            _screenView.Enable();
            _resources.inGameObject.SetActive(true);
        }

        private void OnLevelEndStart()
        {
            _resources.inGameObject.SetActive(false);
            _levelEndPresenter.Initialize();
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
            _scorePresenter.OnScoreUpdated -= _screenView.SetScore;
            _gamePlayPresenter.OnMoveCommand -= UpdateButtonStatus;
        }
    }
}