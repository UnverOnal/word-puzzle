using UnityEngine;

namespace UI.Screens.LevelSelection
{
    [CreateAssetMenu(fileName = "LevelSelectionAssets", menuName = "ScriptableObjects/LevelSelectionAssets")]
    public class LevelSelectionAssets : ScriptableObject
    {
        public GameObject levelUiPrefab;
    }
}
