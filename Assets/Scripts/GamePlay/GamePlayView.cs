using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameManagement;
using GamePlay.TileSystem;
using UnityEngine;

namespace GamePlay
{
    public class GamePlayView
    {
        private readonly GameSettings _gameSettings;

        public GamePlayView(GameSettings gameSettings)
        {
            _gameSettings = gameSettings;
        }

        public async UniTask Vibrate(List<LetterTile> letterTiles)
        {
            List<UniTask> tasks = new List<UniTask>();
            
            for (int i = 0; i < letterTiles.Count; i++)
            {
                var letterTile = letterTiles[i];
                var task = VibrateTile(letterTile.GameObject.transform);
                tasks.Add(task);
            }

            await UniTask.WhenAll(tasks);
        }

        private async UniTask VibrateTile(Transform transform)
        {
            var originalPosition = transform.position;
            float elapsedTime = 0f;
        
            while (elapsedTime < _gameSettings.duration)
            {
                float offsetX = Random.Range(-_gameSettings.intensity, _gameSettings.intensity);
                float offsetY = Random.Range(-_gameSettings.intensity, _gameSettings.intensity);
        
                transform.position = originalPosition + new Vector3(offsetX, offsetY, 0f);
        
                elapsedTime += Time.deltaTime;
                await UniTask.Yield();
            }
        
            transform.position = originalPosition;
        }
    }
}
