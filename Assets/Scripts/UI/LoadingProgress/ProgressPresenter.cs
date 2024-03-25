using System;
using Services.SceneService;
using VContainer;
using VContainer.Unity;

namespace UI.LoadingProgress
{
    public class ProgressPresenter : IInitializable, IDisposable
    {
        [Inject] private readonly ISceneService _sceneService;

        private readonly ProgressView _progressView;

        [Inject]
        public ProgressPresenter(ProgressResources progressResources)
        {
            _progressView = new ProgressView(progressResources);
        }

        public void Initialize()
        {
            _sceneService.OnProgressUpdated += OnProgressUpdated;
        }

        private void OnProgressUpdated(float progress)
        {
            _progressView.UpdateProgress(progress);
        }

        public void Dispose()
        {
            _sceneService.OnProgressUpdated -= OnProgressUpdated;
        }
    }
}