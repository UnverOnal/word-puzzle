using GamePlay;
using UnityEngine;

namespace GameManagement
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/GameSettings")]
    public class GameSettings : ScriptableObject
    {
        [Header("Camera")]
        public float cameraSizeOffset;
        public Vector3 cameraPositionOffset;
        
        [Header("Forming Area")]
        public Vector3 formingAreaOffset;
        public int formingAreaSize;
        
        [Header("")]
        public MoveData moveData;

        [Header("Vibration")] 
        public float intensity;

        public float duration;
    }
}