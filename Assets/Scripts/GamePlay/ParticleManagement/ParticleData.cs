using System;
using UnityEngine;

namespace GamePlay.ParticleManagement
{
    [CreateAssetMenu(menuName = "ScriptableObjects/ParticleData", fileName = "ParticleData")]
    public class ParticleData : ScriptableObject
    {
        public Particle[] particles;
    }

    [Serializable]
    public struct Particle
    {
        public ParticleType type;
        public GameObject particle;
    }

    public enum ParticleType
    {
        DestroyTile,
        None
        
    }
}
