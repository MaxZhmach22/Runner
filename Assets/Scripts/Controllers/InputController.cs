using UniRx;
using UnityEngine;


namespace Runner
{
    internal sealed class InputController : BaseController, IInputController
    {
        private Vector2 _startPosition;
        private Vector2 _endPosition;
        private Vector2 _movedVector;
        private Player _player;
        private CompositeDisposable _disposable = new CompositeDisposable();
        private Subject<Directions> _directionToMove = new Subject<Directions>();

        public Subject<Directions> DirectionToMove => _directionToMove;

        public InputController(Player player) =>
            _player = player;

        public override void Start()
        {
            var swipeStream = Observable.EveryUpdate()
                 .Where(_ => Input.touchCount == 1)
                 .Select(_ => Input.GetTouch(0))
                 .Subscribe(touch => SwipeLenght(touch)).AddTo(_disposable);
        }

        public override void Dispose() =>
             _disposable.Clear();

        private void SwipeLenght(Touch touch)
        {
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    _startPosition = touch.position;
                    break;
                case TouchPhase.Moved:
                    _movedVector = touch.position;
                    break;
                case TouchPhase.Ended:
                    _endPosition = touch.position - _startPosition;
                    var distanceX = Mathf.Abs(touch.position.x) - Mathf.Abs(_startPosition.x); 
                    if(Mathf.Abs(distanceX) > _player.SwipeDeadZone)
                    {
                        Debug.Log($"Distance {distanceX} SwipeDeadZOne {_player.SwipeDeadZone}");
                        CheckDirection();
                    }
                    break;
                default:
                    break;
            }
        }

        private void CheckDirection()
        {
            if (_player.InverseSwipeDirection)
                _directionToMove?.OnNext(_endPosition.x > 0 ? Directions.Left : Directions.Right);
            else
                _directionToMove?.OnNext(_endPosition.x > 0 ? Directions.Right : Directions.Left);
        }
    }
}
