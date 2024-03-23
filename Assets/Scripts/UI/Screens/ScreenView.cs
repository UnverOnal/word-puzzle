namespace UI.Screens
{
    public class ScreenView
    {
        public bool IsActive { get; private set; }
        private readonly Panel _panel;

        protected ScreenView(ScreenResources screenResources)
        {
            _panel = new Panel(screenResources.screen, screenResources.screenGameObject);
        }

        public void Enable()
        {
            IsActive = true;
            _panel.EnablePanel(instant:false);
        }

        public void Disable()
        {
            IsActive = false;
            _panel.DisablePanel(instant : false);
        }
    }
}
