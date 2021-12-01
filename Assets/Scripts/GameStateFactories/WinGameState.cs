using UnityEngine;
using Zenject;


namespace Runner
{
    internal sealed class WinGameState : GameState
    {
        #region Fields

        private readonly WinGameController _winGameController;

        #endregion


        #region ClassLifeCycles

        public WinGameState(WinGameController winGameController) =>
            _winGameController = winGameController;

        public override void Start() =>
            _winGameController.Start();

        public override void Dispose() =>
            _winGameController.Dispose();

        #endregion


        #region Methods

        public override void Update() { }

        #endregion


        internal class Factory : PlaceholderFactory<WinGameState>
        {
        }
    }
}
