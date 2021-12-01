namespace Runner
{
    internal sealed class LevelController : BaseController, ILevelController
    {
        public BaseLevel CurrentLevel => _currentLevel;

        private readonly LevelsInitializations _levelsInitializations;
        private readonly GameData _gameData;
        private BaseLevel _currentLevel;
        private int _levelNumber;

        public LevelController(
            LevelsInitializations levelsInitializations, 
            GameData gameData,
            Player player)
        {
            _levelsInitializations = levelsInitializations;
            _gameData = gameData;
            _levelNumber = 0;
            _currentLevel = _levelsInitializations.Levels[_levelNumber];
        }

        public void SwitchToNextLevel()
        {
            _currentLevel.gameObject.SetActive(false);
            _levelNumber++;
            if(_levelNumber >= _gameData.LevelsList.Count)
            {
                _levelNumber = 0;
                _currentLevel = _levelsInitializations.Levels[_levelNumber];
            }
            _currentLevel = _levelsInitializations.Levels[_levelNumber];
            _currentLevel.gameObject.SetActive(true);
        }

        public override void Start()
        {
            if (_gameData.TestLevelMode)
                _currentLevel = _levelsInitializations.Levels[_gameData.StartWithLevel - 1];

            _currentLevel.gameObject.SetActive(true);
        }

        public override void Dispose()
        {
            foreach (var interactableItem in _currentLevel.InteractableObjects)
                interactableItem.gameObject.SetActive(true);
        }
    }
}