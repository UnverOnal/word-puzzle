using GameState;

namespace UI.Screens.Home
{
    public class HomeScreenView : ScreenView
    {
        private readonly GameStatePresenter _statePresenter;
        private HomeScreenResources _resources;
        
        public HomeScreenView(ScreenResources screenResources, GameStatePresenter statePresenter) : base(screenResources)
        {
            _statePresenter = statePresenter;
            _resources = (HomeScreenResources)screenResources;
        }

        public void OnLevelsButtonClicked()
        {
            _statePresenter.UpdateGameState(GameState.GameState.LevelSelection);
            Disable();
        }
    }
}
