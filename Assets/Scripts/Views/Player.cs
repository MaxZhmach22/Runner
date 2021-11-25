using UnityEngine;
using Zenject;


namespace PiggerBomber
{
    internal class Player : MonoBehaviour
    {
        #region Fields


        private GameState _state;
        private GameStateFactory _gameStateFactory;

        public GameStates CurrentGameState { get; private set; }


        #endregion


        #region ClassLifeCycles

        [Inject]
        public void Init(GameStateFactory gameStateFactory)
        {
            _gameStateFactory = gameStateFactory;
        }

        public void Start() =>
            ChangeState(GameStates.Start);


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