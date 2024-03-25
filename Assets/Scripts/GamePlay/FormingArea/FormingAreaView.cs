using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using GamePlay.TileSystem;
using UnityEngine;

namespace GamePlay.FormingArea
{
    public class FormingAreaView
    {
        public async UniTask DestroyWord(List<LetterTile> tiles)
        {
            var tasks = new List<UniTask>();
            for (var i = 0; i < tiles.Count; i++)
            {
                var tile = tiles[i];
                var transform = tile.GameObject.transform;
                var targetValue = new Vector3(0f, 0f, transform.localScale.z);
                var task = transform.DOScale(targetValue, 0.35f).SetEase(Ease.InBack).ToUniTask();
                tasks.Add(task);
            }

            await UniTask.WhenAll(tasks);
        }
    }
}