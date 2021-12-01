using UnityEngine;
using Zenject;
using UniRx;


namespace Runner
{
    internal sealed class PlayerMoveController : BaseController, ITickable, IFixedTickable
    {
        #region Fields

        private int _currentPlayersTrack; 
        private Vector3 _sideWayDiection;
        private readonly Player _player;
        private readonly ILevelController _levelController;
        private readonly IInputController _inputController;
        private readonly CompositeDisposable _disposables = new CompositeDisposable();

        #endregion


        #region ClassLifeCycles

        public PlayerMoveController(
            Player player,
            ILevelController levelController,
            IInputController inputController)
        {
            _player = player;
            _levelController = levelController;
            _inputController = inputController;
        }

        public override void Start()
        {
            _inputController.DirectionToMove.Subscribe(direction => SideWaysMove(direction)).AddTo(_disposables);
            ResetValues();
        }

        public override void Dispose() =>
            _disposables.Clear();

        #endregion


        #region ZenjectMethods

        public void Tick()
        {
            if (_player.IsDead || _player.IsWin)
                return;

            if (_player.transform.position.y < 0)
                _player.Dead();
        }

        public void FixedTick()
        {
            if (_player.IsDead || _player.IsWin)
                return;

            var direction = _sideWayDiection - _player.transform.position;
            _player.Rigidbody.MovePosition(_player.transform.position + SideWayMoveVector(direction) + ForwardMoveVector());
        }


        #endregion


        #region Methods

        private void ResetValues()
        {
            _currentPlayersTrack = _levelController.CurrentLevel.PlayerStartTrackNumber;
            _player.transform.position = PlayersStartTrackPosition();
            _sideWayDiection = ChangeTrack(Directions.None);
        }

        private void SideWaysMove(Directions direction)
        {
            switch (direction)
            {
                case Directions.Left:
                    if (_currentPlayersTrack + (int)direction > 0)
                    {
                        _sideWayDiection = ChangeTrack(direction);
                        _currentPlayersTrack += (int)direction;
                        break;
                    }
                    else
                       break;
                case Directions.Right:
                    if (_currentPlayersTrack + (int)direction <= _levelController.CurrentLevel.Tracks.Count)
                    {
                        _sideWayDiection = ChangeTrack(direction);
                        _currentPlayersTrack += (int)direction;
                        break;
                    }
                    else
                        break;
            }
        }

        private Vector3 ForwardMoveVector() =>
            Vector3.forward * _player.Speed * Time.deltaTime;

        private Vector3 SideWayMoveVector(Vector3 direction) =>
            new Vector3(direction.x, 0, 0) * _player.SideWayForce * Time.deltaTime;

        private Vector3 ChangeTrack(Directions direction) =>
            new Vector3(
                _levelController.CurrentLevel.Tracks[(_currentPlayersTrack - 1) + (int)direction]
                .position.x,
                0, 
                0);

        private Vector3 PlayersStartTrackPosition() =>
           new Vector3(
                _levelController.CurrentLevel.Tracks[_currentPlayersTrack - 1]
                .position.x,
                0.5f,
                0);

        #endregion
    } 
}
