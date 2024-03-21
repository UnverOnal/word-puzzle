using System.Collections.Generic;
using Services.PoolingService;
using UnityEngine;

namespace UI.Screens.LevelSelection
{
    public class LevelSelectionView : ScreenView
    {
        private readonly GameObject _levelUiPrefab;
        private readonly LevelSelectionResources _levelSelectionResources;

        private readonly ObjectPool<GameObject> _levelUiPool;

        public LevelSelectionView(ScreenResources screenResources, GameObject levelUiPrefab, IPoolService poolService) : base(screenResources)
        {
            _levelUiPrefab = levelUiPrefab;
            _levelSelectionResources = (LevelSelectionResources)screenResources;

            var poolFactory = poolService.GetPoolFactory();
            _levelUiPool = poolFactory.CreatePool(LevelUiCreator);
        }

        public void DisplayLevels(List<TempData> levelDatas)
        {
            for (int i = 0; i < levelDatas.Count; i++)
            {
                var levelData = levelDatas[i];
                
                var levelUiGameObject = _levelUiPool.Get();
                var levelUiResources = levelUiGameObject.GetComponent<LevelUiResources>();
                
                SetLevelUiData(levelUiResources, levelData);
            }
        }

        private void SetLevelUiData(LevelUiResources levelUiResources, TempData levelData)
        {
            levelUiResources.playButton.onClick.AddListener(Disable);
            levelUiResources.scoreText.text = "Score : " + levelData.score;
            levelUiResources.titleLevelCountText.text = "Level " + levelData.levelCount + " - " + levelData.title;
        }

        private GameObject LevelUiCreator()
        {
            var contentTransform = _levelSelectionResources.contentTransform;
            var levelUi = Object.Instantiate(_levelUiPrefab, parent: contentTransform);
            return levelUi;
        }
    }
}
