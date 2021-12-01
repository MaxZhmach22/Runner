﻿using UnityEngine;
using Zenject;
using UniRx;


namespace Runner
{
    internal class Player : MonoBehaviour
    {
        #region Fields
        [field: SerializeField] public bool InverseSwipeDirection { get; private set; }
        [field: SerializeField] public float SwipeDeadZone { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float SideWayForce { get; private set; }
        [field: SerializeField] public float TimeScaleSlowDownTimer { get; private set; }
        [SerializeField] private GameObject _virtualCamera;

        private GameState _state;
        private GameStateFactory _gameStateFactory;
        private Collider[] _colliders;
        private Rigidbody[] _rigidBodies;
        private float _startSpeed;

        public Subject<float> InteractableItemValue = new Subject<float>();
        public Rigidbody Rigidbody => _rigidBodies[0];
        private Animator _animator;
        public bool IsDead { get; private set; }
        public bool IsWin { get; private set; }

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
            _startSpeed = Speed;
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
            if (!active)
                _rigidBodies[0].constraints = RigidbodyConstraints.FreezeAll;
            else
                _rigidBodies[0].constraints = RigidbodyConstraints.FreezeRotation;
        }

        public void Start()
        {
            gameObject.SetActive(false);
            ChangeState(GameStates.Game);
        }

        #endregion

        private void OnCollisionEnter(Collision collision)
        {
            if (IsDead)
                return;

            if (collision.gameObject.layer == (int)PhysicsLayers.Obstacle)
            {
                Dead();
                Debug.Log(gameObject.name);
            }
            if (collision.gameObject.CompareTag("Finish"))
            {
                Debug.Log("Finish");
                IsWin = true;
                _virtualCamera.SetActive(false);
                ChangeState(GameStates.Win);
            }
            if(collision.gameObject.layer == (int)PhysicsLayers.Interactable)
            {
                if(collision.gameObject.TryGetComponent<BaseInteractable>(out var baseInteractable))
                {
                    InteractableItemValue?.OnNext(baseInteractable.Value);
                    Booster booster = baseInteractable as Booster;
                    if (booster != null)
                    {
                        Speed += booster.IncreaseSpeed;
                        _animator.SetFloat("RunSpeedAnimation", Mathf.Clamp(Speed, 2f,4f));
                    } 
                    Slower slower = baseInteractable as Slower;
                    if (slower != null)
                    {
                        Speed -= slower.DecreaseSpeed;
                        _animator.SetFloat("RunSpeedAnimation", Mathf.Clamp(Speed, 2f, 4f));
                    }
                }
                collision.gameObject.SetActive(false);
            }
        }


        #region Methods

        public void ChangeState(GameStates state)
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
            SetMain(true);
            _virtualCamera.SetActive(true);
        }

        public void Dead()
        {
            if (IsDead)
                return;

            IsDead = true;
            SetRagdollEnabled(true);
            SetMain(false);
            _virtualCamera.SetActive(false);
            ChangeState(GameStates.Loose);
        }


        #endregion
    }
}