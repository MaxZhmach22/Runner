﻿namespace Runner
{
    internal sealed class LevelController : BaseController, ILevelController
    {
        private LevelsInitializations _levelsInitializations;
        private GameData _gameData;
        private BaseLevel _currentLevel;
        private int _levelNumber;
        public BaseLevel CurrentLevel => _currentLevel;

        public LevelController(
            LevelsInitializations levelsInitializations, 
            GameData gameData)
        {
            _levelsInitializations = levelsInitializations;
            _gameData = gameData;
            _levelNumber = _gameData.StartWithLevel - 1;
        }

        public void SwitchToNextLevel()
        {
            _currentLevel.gameObject.SetActive(false);
            _levelNumber++;
            if(_levelNumber > _gameData.LevelsList.Count)
            {
                _levelNumber = 0;
                _currentLevel = _levelsInitializations.Levels[_levelNumber];
            }
            _currentLevel = _levelsInitializations.Levels[_levelNumber];
            _currentLevel.gameObject.SetActive(true);
        }

        public override void Start()
        {
            _currentLevel = _levelsInitializations.Levels[_gameData.StartWithLevel - 1];
            _currentLevel.gameObject.SetActive(true);
        }

        public override void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}