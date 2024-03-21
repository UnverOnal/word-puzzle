using VContainer;

namespace UI.Screens.Home
{
    public class HomeScreenPresenter : IScreenPresenter
    {
        private readonly HomeScreenResources _resources;
        private readonly HomeScreenView _screenView;

        [Inject]
        public HomeScreenPresenter(HomeScreenResources resources)
        {
            _resources = resources;
            _screenView = new HomeScreenView(resources);
        }

        public void Initialize()
        {
            _resources.levelsButton.onClick.AddListener(() => _screenView.Disable());
        }
    }
}