using GamePlay;
using UnityEngine;

namespace GameManagement
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/GameSettings")]
    public class GameSettings : ScriptableObject
    {
        public float cameraSizeOffset;
        public Vector3 cameraPositionOffset;
        public Vector3 formingAreaOffset;
        public int formingAreaSize;
        public MoveData moveData;
    }
}