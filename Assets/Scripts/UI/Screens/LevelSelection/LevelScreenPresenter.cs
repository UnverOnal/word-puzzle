using Cysharp.Threading.Tasks;
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
            LevelScreenAssets levelScreenAssets, GameStatePresenter statePresenter, LevelPresenter levelPresenter,
            IDataStorageService dataStorageService) : base(statePresenter)
        {
            _levelScreenModel = new LevelScreenModel(levelPresenter.LevelCreationDatas, dataStorageService);

            _screenView =
                new LevelScreenView(resources, levelScreenAssets.levelUiPrefab, statePresenter, levelPresenter);
        }

        public void Initialize()
        {
            SetStateAction();
        }

        protected override async void OnStateUpdate(GameState.GameState gameState)
        {
            if (gameState == GameState.GameState.LevelSelection)
            {
                await SetLevelUis();
                _screenView.Enable();
            }
            else if (_screenView.IsActive)
            {
                _screenView.Disable();
            }
        }

        private async UniTask SetLevelUis()
        {
            await _levelScreenModel.SetLevelData();
            _screenView.DisplayLevels(_levelScreenModel.LevelDisplayDatas);
        }
    }
}