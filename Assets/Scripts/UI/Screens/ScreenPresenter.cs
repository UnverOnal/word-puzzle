using System;
using GameState;
using VContainer;

namespace UI.Screens
{
    public abstract class ScreenPresenter : IDisposable
    {
        private readonly GameStatePresenter _gameStatePresenter;

        [Inject]
        protected ScreenPresenter(GameStatePresenter gameStatePresenter)
        {
            _gameStatePresenter = gameStatePresenter;
        }

        protected void SetStateAction()
        {
            _gameStatePresenter.OnStateUpdate += OnStateUpdate;
        }

        protected abstract void OnStateUpdate(GameState.GameState gameState);

        public void Dispose()
        {
            _gameStatePresenter.OnStateUpdate += OnStateUpdate;
        }
    }
}
