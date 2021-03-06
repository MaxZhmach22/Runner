using System.Collections.Generic;
using UnityEngine;


namespace Runner
{
    internal abstract class BaseLevel : MonoBehaviour
    {
        #region Fields

        [field: SerializeField] public List<Transform> Tracks { get; private set; }
        [field: SerializeField] public int PlayerStartTrackNumber { get; private set; }
        [field: SerializeField] public List<GameObject> InteractableObjects { get; private set; }


        #endregion

        #region UnityMethods

        private void OnValidate()
        {
            if (PlayerStartTrackNumber > Tracks.Count)
                PlayerStartTrackNumber = Tracks.Count;
        } 
        #endregion
    }
}

