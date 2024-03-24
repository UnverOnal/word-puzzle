
namespace UI.Screens.Game.LevelEnd
{
    public class LevelEndPresenter
    {
        private readonly LevelEndResources _resources;
        private readonly LevelEndView _levelEndView;
        private readonly LevelEndModel _levelEndModel;

        public LevelEndPresenter(LevelEndResources resources)
        {
            _resources = resources;
            _levelEndView = new LevelEndView(resources);
            _levelEndModel = new LevelEndModel();
        }

        public void Initialize()
        {
            _resources.levelEndGameObject.SetActive(true);
            _levelEndView.SetHighScore(_levelEndModel.highScore);
        }

        public void Close()
        {
            _resources.levelEndGameObject.SetActive(false);
        }
    }
}