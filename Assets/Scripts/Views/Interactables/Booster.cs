using UnityEngine;


namespace Runner
{
    internal sealed class Booster : BaseInteractable
    {
        #region Fields

        [field: SerializeField] public float IncreaseSpeed { get; private set; } 

        #endregion
    }
}
