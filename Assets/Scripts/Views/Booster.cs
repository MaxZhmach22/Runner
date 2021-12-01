using UnityEngine;


namespace Runner
{
    internal sealed class Booster : BaseInteractable
    {
        [field: SerializeField] public float IncreaseSpeed { get; private set; }
    }
}
