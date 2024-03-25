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

        private readonly GameObject[] _levelUis;
        private readonly LevelUiResources[] _levelUiResources;

        public LevelScreenView(ScreenResources screenResources, GameObject levelUiPrefab,
            GameStatePresenter statePresenter, LevelPresenter levelPresenter) : base(screenResources)
        {
            _levelUiPrefab = levelUiPrefab;
            _statePresenter = statePresenter;
            _levelPresenter = levelPresenter;
            _resources = (LevelScreenResources)screenResources;
            
            _playButtons = new Dictionary<GameObject, int>();
            _levelUis = new GameObject[levelPresenter.LevelCreationDatas.Count];
            _levelUiResources = new LevelUiResources[_levelUis.Length];
        }

        public void DisplayLevels(List<LevelDisplayData> levelDisplayDatas)
        {
            var previousPlayStatus = PlayStatus.Played;
            for (var i = 0; i < levelDisplayDatas.Count; i++)
            {
                var levelDisplayData = levelDisplayDatas[i];

                _levelUis[i] ??= LevelUiCreator();
                var levelUi = _levelUis[i];
                
                _levelUiResources[i] ??= levelUi.GetComponent<LevelUiResources>();

                var playButtonObject = _levelUiResources[i].playButton.gameObject;
                if (!_playButtons.ContainsKey(playButtonObject))
                {
                    _playButtons.Add(playButtonObject, i);
                    _levelUiResources[i].playButton.onClick.AddListener(OnPlayButtonClicked);
                }

                SetLevelUiData(_levelUiResources[i], levelDisplayData, previousPlayStatus);
                previousPlayStatus = levelDisplayData.playStatus;
            }
        }

        private void SetLevelUiData(LevelUiResources levelUiResources, LevelDisplayData levelData, PlayStatus previousPlayStatus)
        {
            levelUiResources.playButton.gameObject.SetActive(previousPlayStatus == PlayStatus.Played);
            levelUiResources.lockedObject.SetActive(previousPlayStatus is PlayStatus.Locked or PlayStatus.Playable);

            var scoreText = levelData.playStatus == PlayStatus.Played ? "High Score : " + levelData.highScore
                : previousPlayStatus == PlayStatus.Played ? "No Score"
                : "Locked Level";
            
            levelUiResources.scoreText.text = scoreText;
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