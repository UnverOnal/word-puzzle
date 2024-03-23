using GameState;
using Services.PoolingService;
using VContainer;

namespace UI.Screens.LevelSelection
{
    public class LevelScreenPresenter : ScreenPresenter, IScreenPresenter
    {
        private readonly LevelScreenModel _levelScreenModel;
        private readonly LevelScreenView _screenView;

        [Inject]
        public LevelScreenPresenter(IPoolService poolService, LevelScreenResources resources,
            LevelScreenAssets levelScreenAssets, GameStatePresenter statePresenter) : base(statePresenter)
        {
            _levelScreenModel = new LevelScreenModel();
            _screenView =
                new LevelScreenView(resources, levelScreenAssets.levelUiPrefab, poolService);
        }

        public void Initialize()
        {
            SetStateAction();
            _screenView.DisplayLevels(_levelScreenModel.LevelDatas);
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