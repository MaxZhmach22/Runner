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

        #endregion


        #region ClassLifeCycles

        public ScoreController(Player player) =>
            _player = player;

        public override void Start()
        {
            _score = _scoreOnBeginLevel;
            _onScoreChange.Value = _score;
        }

        public override void Dispose()
        {
            _onScoreChange.Value = _score;
            if (_player.CurrentGameState == GameStates.Loose)
                _score = 0;
            if (_player.CurrentGameState == GameStates.Win)
                _scoreOnBeginLevel = _score;
        }

        #endregion


        #region Methods

        public void ChangeScore(float score)
        {
            _score += score;
            _onScoreChange.Value = _score;
        } 
        #endregion
    }
}