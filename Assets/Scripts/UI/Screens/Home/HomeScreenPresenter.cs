using GameState;
using VContainer;

namespace UI.Screens.Home
{
    public class HomeScreenPresenter : ScreenPresenter, IScreenPresenter
    {
        private readonly HomeScreenResources _resources;
        private readonly HomeScreenView _screenView;

        [Inject]
        public HomeScreenPresenter(HomeScreenResources resources, GameStatePresenter statePresenter) : base(statePresenter)
        {
            _resources = resources;
            _screenView = new HomeScreenView(resources);
        }

        public void Initialize()
        {
            SetStateAction();
            _resources.levelsButton.onClick.AddListener(() => _screenView.Disable());
        }

        protected override void OnStateUpdate(GameState.GameState gameState)
        {
            if(gameState == GameState.GameState.Home)
                _screenView.Enable();
            else if(_screenView.IsActive)
                _screenView.Disable();
        }
    }
}