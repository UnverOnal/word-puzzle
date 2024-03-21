using UI.LoadingProgress;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Scopes
{
    public class LoadingScope : LifetimeScope
    {
        [SerializeField] private ProgressResources progressResources;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(progressResources);
            builder.Register<ProgressPresenter>(Lifetime.Singleton);
        }
    }
}
