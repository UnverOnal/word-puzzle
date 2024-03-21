using VContainer;

namespace UI.Screens.Home
{
    public class HomePresenter : IScreenPresenter
    {
        private readonly HomeResources _homeResources;
        private readonly HomeView _homeView;

        [Inject]
        public HomePresenter(HomeResources homeResources)
        {
            _homeResources = homeResources;
            _homeView = new HomeView(homeResources);
        }

        public void Initialize()
        {
            _homeResources.levelsButton.onClick.AddListener(() => _homeView.Disable());
        }
    }
}