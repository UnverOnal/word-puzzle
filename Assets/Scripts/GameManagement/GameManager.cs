using Dictionary;
using Services.SceneService;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameManagement
{
    public class GameManager : IInitializable
    {
        private readonly ISceneService _sceneService;
        private readonly WordDictionary _wordDictionary;

        [Inject]
        public GameManager(ISceneService sceneService, WordDictionary wordDictionary)
        {
            _sceneService = sceneService;
            _wordDictionary = wordDictionary;
        }

        public void Initialize()
        {
            Application.targetFrameRate = 60;
        
            _sceneService.LoadScene(SceneType.GameScene);
            _wordDictionary.Initialize();
        }
    }
}
