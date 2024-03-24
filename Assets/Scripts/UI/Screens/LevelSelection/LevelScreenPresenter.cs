using GameState;
using LevelCreation;
using VContainer;

namespace UI.Screens.LevelSelection
{
    public class LevelScreenPresenter : ScreenPresenter, IScreenPresenter
    {
        private readonly LevelScreenModel _levelScreenModel;
        private readonly LevelScreenView _screenView;

        [Inject]
        public LevelScreenPresenter(LevelScreenResources resources,
            LevelScreenAssets levelScreenAssets, GameStatePresenter statePresenter, LevelPresenter levelPresenter) : base(statePresenter)
        {
            _levelScreenModel = new LevelScreenModel(levelPresenter.LevelDatas);
            _screenView =
                new LevelScreenView(resources, levelScreenAssets.levelUiPrefab, statePresenter, levelPresenter);
        }

        public void Initialize()
        {
            SetStateAction();
            _screenView.DisplayLevels(_levelScreenModel.LevelDisplayDatas);
        }

        protected override void OnStateUpdate(GameState.GameState gameState)
        {
            if(gameState == GameState.GameState.LevelSelection)
                _screenView.Enable();
            else if(_screenView.IsActive)
                _screenView.Disable();
        }
    }
}