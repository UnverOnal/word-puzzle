using Services.SceneService;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameManagement
{
    public class GameManager : IInitializable
    {
        private readonly ISceneService _sceneService;

        [Inject]
        public GameManager(ISceneService sceneService)
        {
            _sceneService = sceneService;
        }

        public void Initialize()
        {
            Application.targetFrameRate = 60;
        
            _sceneService.LoadScene(SceneType.GameScene);
        }
    }
}
