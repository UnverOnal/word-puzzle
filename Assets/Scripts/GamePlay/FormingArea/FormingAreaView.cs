using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using GamePlay.TileSystem;

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
                var task = transform.DOScale(0f, 0.35f).SetEase(Ease.InBack).ToUniTask();
                tasks.Add(task);
            }

            await UniTask.WhenAll(tasks);
        }
    }
}