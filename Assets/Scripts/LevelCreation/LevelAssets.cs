using UnityEngine;

namespace LevelCreation
{
    [CreateAssetMenu(fileName = "LevelAssets", menuName = "ScriptableObjects/LevelAssets")]
    public class LevelAssets : ScriptableObject
    {
        public TextAsset[] levelDataFiles;
        public GameObject tilePrefab;
        public GameObject emptyTilePrefab;
    }
}
