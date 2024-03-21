using System.Globalization;

namespace UI.LoadingProgress
{
    public class ProgressView
    {
        private readonly ProgressResources _progressResources;

        public ProgressView(ProgressResources progressResources)
        {
            _progressResources = progressResources;
        }

        public void UpdateProgress(float progress)
        {
            _progressResources.slider.value = progress;
            _progressResources.progressText.text = (progress * 100f).ToString(CultureInfo.InvariantCulture);
        }
    }
}
