using UniRx;


namespace Runner
{
    internal sealed class ScoreController : BaseController 
    {
        #region Fields

        public IReactiveProperty<float> OnScoreChange => _onScoreChange;
        private ReactiveProperty<float> _onScoreChange = new ReactiveProperty<float>();
        private readonly Player _player;
        private float _score;
        private float _scoreOnBeginLevel;
        private CompositeDisposable _disposables;

        #endregion


        #region ClassLifeCycles

        public ScoreController(Player player) =>
            _player = player;

        public override void Start()
        {
            _disposables = new CompositeDisposable();
            _player.InteractableItemValue.Subscribe(value => ChangeScore(value)).AddTo(_disposables);
            _score = _scoreOnBeginLevel;
            _onScoreChange.Value = _score;

        }

        public override void Dispose()
        {
            _disposables.Clear();
            _onScoreChange.Value = _score;
            if (_player.CurrentGameState == GameStates.Loose)
                _score = 0;
            if (_player.CurrentGameState == GameStates.Win)
                _scoreOnBeginLevel = _score;
        }

        #endregion


        #region Methods

        private void ChangeScore(float score)
        {
            _score += score;
            _onScoreChange.Value = _score;
        } 
        #endregion
    }
}