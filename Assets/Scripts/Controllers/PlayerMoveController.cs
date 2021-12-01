using UnityEngine;
using Zenject;
using UniRx;


namespace Runner
{
    internal sealed class PlayerMoveController : BaseController, ITickable, IFixedTickable
    {

        private readonly Player _player;
        private readonly ILevelController _levelController;
        private readonly IInputController _inputController;
        private readonly CompositeDisposable _disposables;
        private float _bonusFactor = 1f;
        private bool _isMoveingSideWays;
        private Vector3 _sideWayDiection;
        private int _currentTrack;
        private float _distanceCovered;

        public PlayerMoveController(
            Player player, 
            ILevelController levelController,
            IInputController inputController)
        {
            _player = player;
            _levelController = levelController;
            _inputController = inputController;
            _disposables = new CompositeDisposable();
        }

        public override void Start()
        {
            _inputController.DirectionToMove.Subscribe(direction => SideWaysMove(direction)).AddTo(_disposables);
            ResetValues();
        }

        public override void Dispose()
        {
            _disposables.Clear();
        }

        private void ResetValues()
        {
            _player.transform.position = StartTrackNumberPosition();
            _currentTrack = _levelController.CurrentLevel.PlayerStartTrackNumber;
            _sideWayDiection = new Vector3(_levelController.CurrentLevel.Tracks[_currentTrack - 1].position.x, 0, 0);
        }


        private void SideWaysMove(Directions directions) //TODO!
        {
            switch (directions)
            {
                case Directions.Left:
                    if (_currentTrack + (int)directions > 0)
                    {
                        _sideWayDiection = new Vector3(_levelController.CurrentLevel.Tracks[(_currentTrack - 1) + (int)directions].position.x, 0, 0);
                        _currentTrack += (int)directions;
                        break;
                    }
                    else
                    {
                        break;
                    }
                case Directions.Right:
                    if (_currentTrack + (int)directions <= _levelController.CurrentLevel.Tracks.Count)
                    {
                        _sideWayDiection = new Vector3(_levelController.CurrentLevel.Tracks[(_currentTrack - 1) + (int)directions].position.x, 0, 0);
                        _currentTrack += (int)directions;
                        return;
                    }
                    else
                    {
                        break;
                    }
            }
        }

        public void FixedTick()
        {
            if (_player.IsDead || _player.IsWin)
                return;

            var direction = _sideWayDiection - _player.transform.position;
            _player.Rigidbody.MovePosition(_player.transform.position + SideWayMoveVector(direction) + ForwardMoveVector());
        }

        public void Tick()
        {
            if (_player.IsDead || _player.IsWin)
                return;

            if (_player.transform.position.y < 0)
                _player.Dead();
        }

        private Vector3 ForwardMoveVector() =>
            Vector3.forward * _player.Speed * _bonusFactor * Time.deltaTime;
     
        private Vector3 SideWayMoveVector(Vector3 direction) =>
            new Vector3(direction.x, 0, 0) * _player.SideWayForce* _bonusFactor * Time.deltaTime;
      
        private Vector3 StartTrackNumberPosition() =>
           new Vector3(
                _levelController.CurrentLevel.Tracks[_levelController.CurrentLevel.PlayerStartTrackNumber - 1]
                .position.x,
                0.5f,
                0);

    }
}
