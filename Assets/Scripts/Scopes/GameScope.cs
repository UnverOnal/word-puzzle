using GameManagement;
using GamePlay;
using GamePlay.FormingArea;
using GameState;
using LevelCreation;
using UI;
using UI.Screens;
using UI.Screens.Game;
using UI.Screens.Home;
using UI.Screens.LevelSelection;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Scopes
{
    public class GameScope : LifetimeScope
    {
        [SerializeField] private HomeScreenResources homeScreenResources;
        [SerializeField] private LevelScreenResources levelScreenResources;
        [SerializeField] private GameScreenResources gameScreenResources;
        [SerializeField] private LevelScreenAssets levelScreenAssets;
        [SerializeField] private LevelAssets levelAssets;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<GameSceneManager>();
            builder.Register<UiManager>(Lifetime.Singleton);
            
            RegisterScreens(builder);

            builder.RegisterInstance(levelAssets);
            builder.Register<LevelPresenter>(Lifetime.Singleton);

            builder.Register<GameStatePresenter>(Lifetime.Singleton);
            builder.Register<GamePlayPresenter>(Lifetime.Singleton);
            builder.Register<FormingAreaPresenter>(Lifetime.Singleton);
        }

        private void RegisterScreens(IContainerBuilder builder)
        {
            builder.RegisterInstance(homeScreenResources);
            builder.Register<IScreenPresenter ,HomeScreenPresenter>(Lifetime.Singleton);

            builder.RegisterInstance(levelScreenResources);
            builder.RegisterInstance(levelScreenAssets);
            builder.Register<IScreenPresenter ,LevelScreenPresenter>(Lifetime.Singleton);

            builder.RegisterInstance(gameScreenResources);
            builder.Register<GameScreenPresenter>(Lifetime.Singleton).AsSelf().As<IScreenPresenter>();
        }
    }
}
