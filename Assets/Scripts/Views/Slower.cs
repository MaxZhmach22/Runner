using UnityEngine;


namespace Runner
{
    internal sealed class Slower : BaseInteractable
    {
        [field: SerializeField] public float DecreaseSpeed { get; private set; }
    }
}
