using UI.Screens.Game.LevelEnd;
using UnityEngine;
using VContainer;

namespace UI.Screens.Game
{
    public class GameScreenPresenter : IScreenPresenter
    {
        private readonly GameScreenView _screenView;
        private readonly LevelEndPresenter _levelEndPresenter;
        
        [Inject]
        public GameScreenPresenter(GameScreenResources resources)
        {
            _screenView = new GameScreenView(resources);
            _levelEndPresenter = new LevelEndPresenter(resources.levelEndResources);
        }

        public void Initialize()
        {
        }

        public void UpdateScore(int score)
        {
            _screenView.SetScore(score);
        }

        public void InitializeLevelEnd()
        {
            _levelEndPresenter.Initialize();
        }

        public void UpdateLevelTitle(string title)
        {
            _screenView.SetLevelTitle(title);
        }
    }
}
