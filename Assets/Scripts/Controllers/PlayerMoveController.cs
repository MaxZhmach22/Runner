using System;
using UnityEngine;
using Zenject;
using UniRx;


namespace Runner
{
    internal sealed class PlayerMoveController : BaseController, IFixedTickable
    {

        private readonly Player _player;
        private readonly ILevelController _levelController;
        private readonly IInputController _inputController;
        private readonly CompositeDisposable _disposables;
        private float _bonusFactor = 1f;
        private bool _isMoveingSideWays;
        private Vector3 _sideWayDiection;

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
            _player.transform.position = StartTrackNumberPosition();
        }

        private void SideWaysMove(Directions directions)
        {
            _isMoveingSideWays = true;
            switch (directions)
            {
                case Directions.Left:
                    _sideWayDiection = Vector3.left; 
                    break;
                case Directions.Right:
                    _sideWayDiection = Vector3.right;
                    break;
            }
            Debug.Log(directions);
        }

        public override void Dispose()
        {
            ResetValues();
            _disposables.Clear();
        }

        private void ResetValues()
        {
            _player.transform.position = Vector3.zero;
        }

        public void FixedTick()
        {
            if (_player.isActiveAndEnabled)
            {
                _player.Rigidbody.MovePosition(_player.transform.position + Vector3.forward * _player.Speed * _bonusFactor * Time.deltaTime);
            }
            if (_isMoveingSideWays)
            {
                _player.Rigidbody.MovePosition(_player.transform.position + _sideWayDiection);
                _isMoveingSideWays = false;
            }
        }

        private Vector3 StartTrackNumberPosition() =>
           new Vector3(
                _levelController.CurrentLevel.Tracks[_levelController.CurrentLevel.PlayerStartTrackNumber - 1]
                .position.x,
                0,
                0);
    }
}
