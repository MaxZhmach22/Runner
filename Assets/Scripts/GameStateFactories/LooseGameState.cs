using Zenject;


namespace Runner
{
    internal sealed class LooseGameState : GameState
    {
        #region Fields

        private readonly LooseGameController _looseGameController;

        #endregion


        #region ClassLifeCycles

        public LooseGameState(LooseGameController looseGameController) =>
           _looseGameController = looseGameController;

        public override void Start() =>
            _looseGameController.Start();

        public override void Dispose() =>
            _looseGameController.Dispose();

        #endregion


        #region Methods

        public override void Update() { }

        #endregion


        internal sealed class Factory : PlaceholderFactory<LooseGameState>
        {
        }
    }
}

