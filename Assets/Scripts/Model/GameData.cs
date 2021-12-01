using System.Collections.Generic;
using UnityEngine;


namespace Runner
{
    [CreateAssetMenu(fileName = nameof(GameData), menuName = "Data")]
    internal sealed class GameData : ScriptableObject
    {
        #region Fields

        [field: SerializeField] public List<BaseLevel> LevelsList { get; private set; }
        [field: SerializeField] public int StartWithLevel { get; private set; }
        [field: SerializeField] public bool TestLevelMode { get; private set; }

        #endregion


        #region UnityMethods

        private void OnValidate()
        {
            if (StartWithLevel > LevelsList.Count)
                StartWithLevel = LevelsList.Count;
            if (StartWithLevel == 0)
                StartWithLevel = 1;
        } 

        #endregion
    }
}
