using TMPro;
using UI.Screens.Game.LevelEnd;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens.Game
{
    public class GameScreenResources : ScreenResources
    {
        public GameObject inGameObject;
        public Button undoButton;
        public Button submitButton;
        public TextMeshProUGUI inGameScore;
        public TextMeshProUGUI levelTitleText;
        public Transform correctWordsParent;

        public LevelEndResources levelEndResources;
    }
}