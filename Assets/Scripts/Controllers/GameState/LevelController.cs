namespace Runner
{
    internal sealed class LevelController : BaseController, ILevelController
    {
        #region Fields

        public BaseLevel CurrentLevel => _currentLevel;
        private readonly LevelsInitializations _levelsInitializations;
        private readonly GameData _gameData;
        private BaseLevel _currentLevel;
        private int _levelNumber = 0;

        #endregion


        #region ClassLifeCycles

        public LevelController(
            LevelsInitializations levelsInitializations,
            GameData gameData)
        {
            _levelsInitializations = levelsInitializations;
            _gameData = gameData;
        }

        public override void Start()
        {
            if (_gameData.TestLevelMode)
                _currentLevel = _levelsInitializations.Levels[_gameData.StartWithLevel - 1];
            else
                _currentLevel = _levelsInitializations.Levels[_levelNumber];

            _currentLevel.gameObject.SetActive(true);
        }

        public override void Dispose()
        {
            foreach (var interactableItem in _currentLevel.InteractableObjects)
                interactableItem.SetActive(true);
        }

        #endregion


        #region Methods

        public void SwitchToNextLevel()
        {
            if (_gameData.TestLevelMode)
                return;

            _currentLevel.gameObject.SetActive(false);
            _levelNumber++;
            if (_levelNumber >= _gameData.LevelsList.Count)
            {
                _levelNumber = 0;
                _currentLevel = _levelsInitializations.Levels[_levelNumber];
            }
            _currentLevel = _levelsInitializations.Levels[_levelNumber];
            _currentLevel.gameObject.SetActive(true);
        } 

        #endregion
    }
}