namespace UI.Screens
{
    public class ScreenView
    {
        private readonly Panel _panel;

        protected ScreenView(ScreenResources screenResources)
        {
            _panel = new Panel(screenResources.screen, screenResources.screenGameObject);
        }

        public void Enable()
        {
            _panel.EnablePanel(instant:false);
        }

        public void Disable()
        {
            _panel.DisablePanel(instant : false);
        }
    }
}
