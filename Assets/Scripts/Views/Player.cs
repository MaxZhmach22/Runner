using System;
using UnityEngine;
using Zenject;


namespace Runner
{
    internal class Player : MonoBehaviour
    {
        #region Fields


        private GameState _state;
        private GameStateFactory _gameStateFactory;
        private Collider[] _colliders;
        private Rigidbody[] _rigidBodies;
        public Rigidbody Rigidbody => _rigidBodies[0];
        private Animator _animator; 
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float SwipeDeadZone { get; private set; }

        public GameStates CurrentGameState { get; private set; }


        #endregion


        #region ClassLifeCycles

        [Inject]
        public void Init(GameStateFactory gameStateFactory)
        {
            _gameStateFactory = gameStateFactory;
            _rigidBodies = GetComponentsInChildren<Rigidbody>();
            _colliders = GetComponentsInChildren<Collider>();
            _animator = GetComponent<Animator>();
            SetRagdollEnabled(false);
            SetMain(true);
        }

        private void SetRagdollEnabled(bool active)
        {
            foreach(Rigidbody rigidbody in _rigidBodies)
            {
                rigidbody.isKinematic = !active;
            }
            foreach (Collider collider in _colliders)
            {
                collider.enabled = active;
            }
        }

        private void SetMain(bool active)
        {
            _animator.enabled = active;
            _rigidBodies[0].isKinematic = !active;
            _colliders[0].enabled = active;
        }

        public void Start()
        {
            gameObject.SetActive(false);
            ChangeState(GameStates.Game);
        }
           
        #endregion


        #region Methods

        public void ChangeState(GameStates state)
        {
            if (_state != null)
            {
                _state.Dispose();
                _state = null;
            }
            CurrentGameState = state;
            _state = _gameStateFactory.CreateState(state);
            _state.Start();
        }

      
        #endregion
    }
}