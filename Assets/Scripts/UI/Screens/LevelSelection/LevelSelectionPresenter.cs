using Services.PoolingService;
using VContainer;

namespace UI.Screens.LevelSelection
{
    public class LevelSelectionPresenter : IScreenPresenter
    {
        private readonly LevelSelectionModel _levelSelectionModel;
        private readonly LevelSelectionView _levelSelectionView;

        [Inject]
        public LevelSelectionPresenter(IPoolService poolService, LevelSelectionResources levelSelectionResources,
            LevelSelectionAssets levelSelectionAssets)
        {
            _levelSelectionModel = new LevelSelectionModel();
            _levelSelectionView =
                new LevelSelectionView(levelSelectionResources, levelSelectionAssets.levelUiPrefab, poolService);
        }

        public void Initialize()
        {
            _levelSelectionView.DisplayLevels(_levelSelectionModel.LevelDatas);
        }
    }
}