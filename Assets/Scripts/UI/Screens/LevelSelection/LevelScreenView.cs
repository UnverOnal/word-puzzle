using System.Collections.Generic;
using GameState;
using LevelCreation;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Screens.LevelSelection
{
    public class LevelScreenView : ScreenView
    {
        private readonly GameObject _levelUiPrefab;
        private readonly GameStatePresenter _statePresenter;
        private readonly LevelPresenter _levelPresenter;
        private readonly LevelScreenResources _resources;
        
        private readonly Dictionary<GameObject, int> _playButtons;

        public LevelScreenView(ScreenResources screenResources, GameObject levelUiPrefab,
            GameStatePresenter statePresenter, LevelPresenter levelPresenter) : base(screenResources)
        {
            _levelUiPrefab = levelUiPrefab;
            _statePresenter = statePresenter;
            _levelPresenter = levelPresenter;
            _resources = (LevelScreenResources)screenResources;
            
            _playButtons = new Dictionary<GameObject, int>();
        }

        public void DisplayLevels(List<LevelDisplayData> levelDisplayDatas)
        {
            for (var i = 0; i < levelDisplayDatas.Count; i++)
            {
                var levelData = levelDisplayDatas[i];

                var levelUiGameObject = LevelUiCreator();
                var levelUiResources = levelUiGameObject.GetComponent<LevelUiResources>();

                SetLevelUiData(levelUiResources, levelData);
                
                _playButtons.Add(levelUiResources.playButton.gameObject, i);
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
            var clickedObject = EventSystem.current.currentSelectedGameObject;
            _playButtons.TryGetValue(clickedObject, out var levelIndex);
            _levelPresenter.CreateLevel(levelIndex);
            _statePresenter.UpdateGameState(GameState.GameState.Game);
            Disable();
        }
    }
}