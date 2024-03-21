using Services.InputService;
using Services.PoolingService;
using Services.SceneService;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Scopes
{
    public class MainScope : LifetimeScope
    {
        [SerializeField] private SceneDataContainer sceneDataContainer;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<GameManager>();
            
            builder.RegisterInstance(sceneDataContainer);
            
            InstallServices(builder);
        }

        private void InstallServices(IContainerBuilder builder)
        {
            builder.Register<ISceneService, SceneService>(Lifetime.Singleton);
            builder.Register<IPoolService, PoolService>(Lifetime.Singleton);
            
            builder.Register<InputService>(Lifetime.Singleton).As<IInputService>().AsSelf();
            builder.RegisterEntryPoint<InputEntryPoint>();
        }
    }
}
