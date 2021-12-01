using UnityEngine;


namespace Runner
{
    internal abstract class BaseInteractable : MonoBehaviour
    {
        #region Fields

        [field: SerializeField] public float Value { get; protected set; } 

        #endregion
    }
}
