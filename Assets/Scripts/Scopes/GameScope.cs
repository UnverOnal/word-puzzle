using UI;
using UI.Screens;
using UI.Screens.Home;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Scopes
{
    public class GameScope : LifetimeScope
    {
        [SerializeField] private HomeResources homeResources;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<UiManager>();
            
            builder.RegisterInstance(homeResources);
            builder.Register<IScreenPresenter ,HomePresenter>(Lifetime.Singleton);
        }
    }
}
