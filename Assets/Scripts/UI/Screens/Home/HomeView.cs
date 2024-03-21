namespace UI.Screens.Home
{
    public class HomeView : ScreenView
    {
        private HomeResources _homeResources;
        
        public HomeView(ScreenResources screenResources) : base(screenResources)
        {
            _homeResources = (HomeResources)screenResources;
        }
    }
}
