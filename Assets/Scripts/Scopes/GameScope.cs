using GameManagement;
using GamePlay;
using GamePlay.FormingArea;
using GamePlay.ParticleManagement;
using GamePlay.Score;
using GameState;
using LevelCreation;
using UI;
using UI.Screens;
using UI.Screens.Game;
using UI.Screens.Home;
using UI.Screens.Home.Level;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Scopes
{
    public class GameScope : LifetimeScope
    {
        [SerializeField] private HomeScreenResources homeScreenResources;
        [SerializeField] private GameScreenResources gameScreenResources;
        
        [SerializeField] private LevelScreenAssets levelScreenAssets;
        [SerializeField] private LevelAssets levelAssets;
        [SerializeField] private ScoreData scoreData;
        [SerializeField] private ParticleData particleData;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<GameSceneManager>();
            builder.Register<UiManager>(Lifetime.Singleton);
            
            RegisterScreens(builder);
            RegisterParticleSystem(builder);

            builder.RegisterInstance(levelAssets);
            builder.Register<LevelPresenter>(Lifetime.Singleton);

            builder.Register<GameStatePresenter>(Lifetime.Singleton);
            builder.Register<GamePlayPresenter>(Lifetime.Singleton);
            builder.Register<FormingAreaPresenter>(Lifetime.Singleton);

            builder.RegisterInstance(scoreData);
            builder.Register<ScorePresenter>(Lifetime.Singleton);
        }

        private void RegisterScreens(IContainerBuilder builder)
        {
            builder.RegisterInstance(homeScreenResources);
            builder.RegisterInstance(levelScreenAssets);
            builder.Register<IScreenPresenter ,HomeScreenPresenter>(Lifetime.Singleton);

            builder.RegisterInstance(gameScreenResources);
            builder.Register<GameScreenPresenter>(Lifetime.Singleton).AsSelf().As<IScreenPresenter>();
        }

        private void RegisterParticleSystem(IContainerBuilder builder)
        {
            builder.RegisterInstance(particleData);
            builder.Register<ParticleManager>(Lifetime.Singleton);
        }
    }
}
