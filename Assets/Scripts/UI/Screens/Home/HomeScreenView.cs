namespace UI.Screens.Home
{
    public class HomeScreenView : ScreenView
    {
        private HomeScreenResources _resources;
        
        public HomeScreenView(ScreenResources screenResources) : base(screenResources)
        {
            _resources = (HomeScreenResources)screenResources;
        }
    }
}
