using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using GamePlay.ParticleManagement;
using GamePlay.TileSystem;
using UnityEngine;

namespace GamePlay.FormingArea
{
    public class FormingAreaView
    {
        private readonly ParticleManager _particleManager;

        public FormingAreaView(ParticleManager particleManager)
        {
            _particleManager = particleManager;
        }

        public async UniTask DestroyWord(List<LetterTile> tiles)
        {
            var tasks = new List<UniTask>();
            for (var i = 0; i < tiles.Count; i++)
            {
                var tile = tiles[i];
                var transform = tile.GameObject.transform;
                var targetValue = new Vector3(0f, 0f, transform.localScale.z);
                var task = transform.DOScale(targetValue, 0.1f).SetEase(Ease.InBack).ToUniTask();
                _particleManager.Play(ParticleType.DestroyTile, transform.position);
                tasks.Add(task);
            }

            await UniTask.WhenAll(tasks);
        }
    }
}