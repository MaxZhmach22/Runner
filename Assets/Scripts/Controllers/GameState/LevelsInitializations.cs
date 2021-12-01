using System;
using System.Collections.Generic;
using UnityEngine;


namespace Runner
{
    internal sealed class LevelsInitializations
    {
        #region Fields

        public List<BaseLevel> Levels => _levels;
        private readonly GameData _gameData;
        private List<BaseLevel> _levels;
        private Transform _parentTransform;

        #endregion


        #region ClassLifeCycles

        public LevelsInitializations(GameData gameData)
        {
            _gameData = gameData;
            LevelsInit();
        }

        #endregion


        #region Methods

        private void LevelsInit()
        {
            if (_gameData.LevelsList.Count == 0)
                throw new ApplicationException($"{nameof(_gameData.LevelsList)} is Empty!");

            _parentTransform = new GameObject("Levels").transform;
            _levels ??= new List<BaseLevel>();
            foreach (var levelPrefab in _gameData.LevelsList)
            {
                var level = GameObject.Instantiate<BaseLevel>(levelPrefab, _parentTransform);
                level.gameObject.SetActive(false);
                _levels.Add(level);
            }
        } 

        #endregion
    }
}