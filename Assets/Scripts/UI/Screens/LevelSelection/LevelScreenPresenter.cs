using GameState;
using LevelCreation;
using Services.DataStorageService;
using VContainer;

namespace UI.Screens.LevelSelection
{
    public class LevelScreenPresenter : ScreenPresenter, IScreenPresenter
    {
        private readonly LevelScreenModel _levelScreenModel;
        private readonly LevelScreenView _screenView;

        [Inject]
        public LevelScreenPresenter(LevelScreenResources resources,
            LevelScreenAssets levelScreenAssets, GameStatePresenter statePresenter, LevelPresenter levelPresenter, IDataStorageService dataStorageService) : base(statePresenter)
        {
            _levelScreenModel = new LevelScreenModel(levelPresenter.LevelDatas, dataStorageService);
            _screenView =
                new LevelScreenView(resources, levelScreenAssets.levelUiPrefab, statePresenter, levelPresenter);
        }

        public async void Initialize()
        {
            SetStateAction();
            await _levelScreenModel.SetLevelData();
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