using System.Collections.Generic;
using GameState;
using Services.PoolingService;
using UnityEngine;

namespace UI.Screens.LevelSelection
{
    public class LevelScreenView : ScreenView
    {
        private readonly GameObject _levelUiPrefab;
        private readonly GameStatePresenter _statePresenter;
        private readonly LevelScreenResources _resources;

        private readonly ObjectPool<GameObject> _levelUiPool;

        public LevelScreenView(ScreenResources screenResources, GameObject levelUiPrefab, IPoolService poolService,
            GameStatePresenter statePresenter) : base(screenResources)
        {
            _levelUiPrefab = levelUiPrefab;
            _statePresenter = statePresenter;
            _resources = (LevelScreenResources)screenResources;

            var poolFactory = poolService.GetPoolFactory();
            _levelUiPool = poolFactory.CreatePool(LevelUiCreator);
        }

        public void DisplayLevels(List<LevelDisplayData> levelDisplayDatas)
        {
            for (var i = 0; i < levelDisplayDatas.Count; i++)
            {
                var levelData = levelDisplayDatas[i];

                var levelUiGameObject = _levelUiPool.Get();
                var levelUiResources = levelUiGameObject.GetComponent<LevelUiResources>();

                SetLevelUiData(levelUiResources, levelData);
            }
        }

        private void SetLevelUiData(LevelUiResources levelUiResources, LevelDisplayData levelData)
        {
            levelUiResources.playButton.onClick.AddListener(OnPlayButtonClicked);
            levelUiResources.scoreText.text = "High Score : " + levelData.score;
            levelUiResources.titleLevelCountText.text =
                "Level " + levelData.levelCount + " - " + UiUtil.FormatString(levelData.title);
        }

        private GameObject LevelUiCreator()
        {
            var contentTransform = _resources.contentTransform;
            var levelUi = Object.Instantiate(_levelUiPrefab, contentTransform);
            return levelUi;
        }

        private void OnPlayButtonClicked()
        {
            _statePresenter.UpdateGameState(GameState.GameState.Game);
            Disable();
        }
    }
}