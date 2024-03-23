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
        private readonly DictionaryPreprocessor _dictionaryPreprocessor;

        [Inject]
        public GameManager(ISceneService sceneService, DictionaryPreprocessor dictionaryPreprocessor)
        {
            _sceneService = sceneService;
            _dictionaryPreprocessor = dictionaryPreprocessor;
        }

        public void Initialize()
        {
            Application.targetFrameRate = 60;
        
            _sceneService.LoadScene(SceneType.GameScene);
            _dictionaryPreprocessor.Initialize();
        }
    }
}
