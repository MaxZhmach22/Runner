using UnityEngine;
using Zenject;


namespace Runner
{
    internal class Player : MonoBehaviour
    {
        #region Fields


        private GameState _state;
        private GameStateFactory _gameStateFactory;
        private Rigidbody _rigidbody;
        public Rigidbody Rigidbody => _rigidbody;
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float SwipeDeadZone { get; private set; }

        public GameStates CurrentGameState { get; private set; }


        #endregion


        #region ClassLifeCycles

        [Inject]
        public void Init(GameStateFactory gameStateFactory)
        {
            _gameStateFactory = gameStateFactory;
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Start() =>
            ChangeState(GameStates.Game);


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