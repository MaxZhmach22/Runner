using UnityEngine;
using Zenject;


namespace Runner
{
    internal sealed class StartGameState : GameState
    {
        #region Fields

        private MainMenuController _mainMenuController;
        private readonly MainMenuController.Factory _mainMenuControllerFactory; 

        #endregion



        #region ClassLifeCycles

        public StartGameState(MainMenuController.Factory mainMenuControllerFactory) =>
            _mainMenuControllerFactory = mainMenuControllerFactory;

        public override void Start()
        {
            Time.timeScale = 1f;
            _mainMenuController = _mainMenuControllerFactory.Create();
            _mainMenuController.Start();
        }

        public override void Dispose() =>
            _mainMenuController.Dispose();

        #endregion

        public override void Update() { }


        internal class Factory : PlaceholderFactory<StartGameState>
        {
        }
    }
}
