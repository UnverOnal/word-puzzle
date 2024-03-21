using UI;
using UI.Screens;
using UI.Screens.Home;
using UI.Screens.LevelSelection;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Scopes
{
    public class GameScope : LifetimeScope
    {
        [SerializeField] private HomeResources homeResources;
        [SerializeField] private LevelSelectionResources levelSelectionResources;
        [SerializeField] private LevelSelectionAssets levelSelectionAssets;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<UiManager>();
            
            RegisterScreens(builder);
        }

        private void RegisterScreens(IContainerBuilder builder)
        {
            builder.RegisterInstance(homeResources);
            builder.Register<IScreenPresenter ,HomePresenter>(Lifetime.Singleton);

            builder.RegisterInstance(levelSelectionResources);
            builder.RegisterInstance(levelSelectionAssets);
            builder.Register<IScreenPresenter ,LevelSelectionPresenter>(Lifetime.Singleton);
        }
    }
}
