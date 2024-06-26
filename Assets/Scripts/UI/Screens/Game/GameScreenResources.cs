using TMPro;
using UI.Screens.Game.LevelEnd;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens.Game
{
    public class GameScreenResources : ScreenResources
    {
        public GameObject inGameObject;
        public HoldableButton undoButton;
        public Button submitButton;
        public TextMeshProUGUI inGameScore;
        public TextMeshProUGUI levelTitleText;

        public LevelEndResources levelEndResources;
    }
}