namespace UI.Screens
{
    public class ScreenView
    {
        private readonly Panel _panel;

        protected ScreenView(ScreenResources screenResources)
        {
            _panel = new Panel(screenResources.screen, screenResources.screenGameObject);
        }

        public void Enable(bool instant = false)
        {
            _panel.EnablePanel(instant:instant);
        }

        public void Disable(bool instant = false)
        {
            _panel.DisablePanel(instant : instant);
        }
    }
}
