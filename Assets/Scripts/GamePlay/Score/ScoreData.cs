using System;
using UnityEngine;

namespace GamePlay.Score
{
    [CreateAssetMenu(fileName = "ScoreData", menuName = "ScriptableObjects/ScoreData")]
    public class ScoreData : ScriptableObject
    {
        public int fixedFactor = 10;
        public int punishmentPoint = 100;
        public LetterData[] letterDatas;
    }

    [Serializable]
    public struct LetterData
    {
        public int point;
        public char letter;
    }
}
