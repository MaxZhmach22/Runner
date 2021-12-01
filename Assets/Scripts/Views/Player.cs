using UnityEngine;
using Zenject;
using UniRx;


namespace Runner
{
    internal class Player : MonoBehaviour
    {
        #region InspectorFields

        [field: Header("Input Settings:")]
        [field: SerializeField] public bool InverseSwipeDirection { get; private set; }
        [field: Range(0, 1000)]
        [field: SerializeField] public float SwipeDeadZone { get; private set; }

        [field: Header("Movement Settings:")]
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float SideWayForce { get; private set; }

        [field: Space]
        [SerializeField] private GameObject _virtualCamera;

        #endregion


        #region Fields

        public bool IsDead { get; private set; }
        public bool IsWin { get; private set; }
        public GameStates CurrentGameState { get; private set; }
        public Subject<float> InteractableItemValue = new Subject<float>();
        public Rigidbody Rigidbody => _rigidBodies[0];

        private Collider[] _colliders;
        private Rigidbody[] _rigidBodies;
        private Animator _animator;
        private GameState _state;
        private GameStateFactory _gameStateFactory;
        private float _startSpeed;
       
        #endregion


        #region ClassLifeCycles

        [Inject]
        public void Init(GameStateFactory gameStateFactory)
        {
            _gameStateFactory = gameStateFactory;

            _rigidBodies = GetComponentsInChildren<Rigidbody>();
            _colliders = GetComponentsInChildren<Collider>();
            _animator = GetComponent<Animator>();
            _startSpeed = Speed;
        }
        public void Start()
        {
            gameObject.SetActive(false);
            ChangeGameState(GameStates.Game);
        }

        #endregion


        #region UnityMethods

        private void OnCollisionEnter(Collision collision)
        {
            if (IsDead || IsWin)
                return;

            if (collision.gameObject.layer == (int)PhysicsLayers.Obstacle)
                Dead();

            if (collision.gameObject.CompareTag("Finish"))
            {
                IsWin = true;
                ChangeGameState(GameStates.Win);
            }

            if (collision.gameObject.layer == (int)PhysicsLayers.Interactable)
                CheckInteractableObject(collision);
        }


        #endregion


        #region Methods

        private void SetRagdollEnabled(bool active)
        {
            foreach(Rigidbody rigidbody in _rigidBodies)
                rigidbody.isKinematic = !active;
            foreach (Collider collider in _colliders)
                collider.enabled = active;
        }

        private void SetMainBodyEnabled(bool active)
        {
            _animator.enabled = active;
            _rigidBodies[0].isKinematic = !active;
            _colliders[0].enabled = active;
            if (!active)
                _rigidBodies[0].constraints = RigidbodyConstraints.FreezeAll;
            else
                _rigidBodies[0].constraints = RigidbodyConstraints.FreezeRotation;
        }

        public void ChangeGameState(GameStates state)
        {
            CurrentGameState = state;
            if (_state != null)
            {
                _state.Dispose();
                _state = null;
            }
            _state = _gameStateFactory.CreateState(state);
            _state.Start();
        }

        public void ResetValues()
        {
            IsDead = false;
            IsWin = false;
            Speed = _startSpeed;
            _animator.SetFloat("RunSpeedAnimation", _startSpeed);
            SetRagdollEnabled(false);
            SetMainBodyEnabled(true);
            _virtualCamera.SetActive(true);
        }

        public void Dead()
        {
            if (IsDead)
                return;

            IsDead = true;
            SetRagdollEnabled(true);
            SetMainBodyEnabled(false);
            _virtualCamera.SetActive(false);
            ChangeGameState(GameStates.Loose);
        }

        private void CheckInteractableObject(Collision collision)
        {
            if (collision.gameObject.TryGetComponent<BaseInteractable>(out var baseInteractable))
            {
                InteractableItemValue?.OnNext(baseInteractable.Value);
                Booster booster = baseInteractable as Booster;
                if (booster != null)
                {
                    Speed = Mathf.Clamp(Speed + booster.IncreaseSpeed, 2f, 4f);
                    _animator.SetFloat("RunSpeedAnimation", Speed);
                }
                Slower slower = baseInteractable as Slower;
                if (slower != null)
                {
                    Speed = Mathf.Clamp(Speed - slower.DecreaseSpeed, 2f, 4f);
                    _animator.SetFloat("RunSpeedAnimation", Speed);
                }
            }
            collision.gameObject.SetActive(false);
        }

        #endregion
    }
}