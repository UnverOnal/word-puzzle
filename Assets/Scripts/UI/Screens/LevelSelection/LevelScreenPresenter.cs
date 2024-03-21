using Services.PoolingService;
using VContainer;

namespace UI.Screens.LevelSelection
{
    public class LevelScreenPresenter : IScreenPresenter
    {
        private readonly LevelScreenModel _levelScreenModel;
        private readonly LevelScreenView _screenView;

        [Inject]
        public LevelScreenPresenter(IPoolService poolService, LevelScreenResources resources,
            LevelScreenAssets levelScreenAssets)
        {
            _levelScreenModel = new LevelScreenModel();
            _screenView =
                new LevelScreenView(resources, levelScreenAssets.levelUiPrefab, poolService);
        }

        public void Initialize()
        {
            _screenView.DisplayLevels(_levelScreenModel.LevelDatas);
        }
    }
}