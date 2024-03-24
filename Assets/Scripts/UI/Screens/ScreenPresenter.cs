using System;
using GameState;

namespace UI.Screens
{
    public abstract class ScreenPresenter : IDisposable
    {
        private readonly GameStatePresenter _gameStatePresenter;

        protected ScreenPresenter(GameStatePresenter gameStatePresenter)
        {
            _gameStatePresenter = gameStatePresenter;
        }

        protected void SetStateAction()
        {
            _gameStatePresenter.OnStateUpdate += OnStateUpdate;
        }

        protected abstract void OnStateUpdate(GameState.GameState gameState);

        public virtual void Dispose()
        {
            _gameStatePresenter.OnStateUpdate += OnStateUpdate;
        }
    }
}
