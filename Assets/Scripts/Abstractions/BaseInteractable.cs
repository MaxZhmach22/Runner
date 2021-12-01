using UnityEngine;


namespace Runner
{
    internal abstract class BaseInteractable : MonoBehaviour
    {
        [field: SerializeField] public float Value { get; protected set; }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
                gameObject.SetActive(false);
        }
    }
}
