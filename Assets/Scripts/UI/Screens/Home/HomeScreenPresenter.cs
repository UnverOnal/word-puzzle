using Cysharp.Threading.Tasks;
using GameState;
using LevelCreation;
using Services.DataStorageService;
using UI.Screens.Home.Level;
using VContainer;

namespace UI.Screens.Home
{
    public class HomeScreenPresenter : ScreenPresenter, IScreenPresenter
    {
        private readonly HomeScreenModel _homeScreenModel;
        private readonly HomeScreenView _screenView;

        [Inject]
        public HomeScreenPresenter(HomeScreenResources resources,
            LevelScreenAssets levelScreenAssets, GameStatePresenter statePresenter, LevelPresenter levelPresenter,
            IDataStorageService dataStorageService) : base(statePresenter)
        {
            _homeScreenModel = new HomeScreenModel(levelPresenter.LevelCreationDatas, dataStorageService);

            _screenView =
                new HomeScreenView(resources, levelScreenAssets.levelUiPrefab, statePresenter, levelPresenter);
        }

        public void Initialize()
        {
            SetStateAction();
            OnStateUpdate(GameState.GameState.Home);
        }

        protected override async void OnStateUpdate(GameState.GameState gameState)
        {
            if (gameState == GameState.GameState.Home)
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
            await _homeScreenModel.SetLevelData();
            _screenView.DisplayLevels(_homeScreenModel.LevelDisplayDatas);
        }
    }
}