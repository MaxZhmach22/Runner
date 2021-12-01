using System;
using Zenject;

namespace Runner
{
    internal sealed class GameGameState : GameState
    {
        #region Fields

        private readonly MainGameController _mainGameController;

        #endregion


        #region ClassLifeCycles

        public GameGameState(MainGameController mainGameController) =>
             _mainGameController = mainGameController;

        public override void Start()
        {
            _mainGameController.Start();
        }

        public override void Dispose() =>
            _mainGameController.Dispose();

        #endregion


        public override void Update() { }

        internal sealed class Factory : PlaceholderFactory<GameGameState>
        {
        }
    }
}
