using UnityEngine;


namespace Runner
{
    internal sealed class Slower : BaseInteractable
    {
        #region Fields

        [field: SerializeField] public float DecreaseSpeed { get; private set; } 

        #endregion
    }
}
