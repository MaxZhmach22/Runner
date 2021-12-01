using ModestTree;


namespace Runner
{
    internal sealed class GameStateFactory
    {
        #region Fields

        readonly WinGameState.Factory _winStateFactory;
        readonly GameGameState.Factory _gameStateFactory;
        readonly LooseGameState.Factory _looseStateFactory; 

        #endregion


        #region ClassLifeCycles

        public GameStateFactory(
          WinGameState.Factory startStateFactory,
          GameGameState.Factory gameStateFactory,
          LooseGameState.Factory endStateFactory)
        {
            _winStateFactory = startStateFactory;
            _gameStateFactory = gameStateFactory;
            _looseStateFactory = endStateFactory;
        }

        #endregion


        #region Methods

        public GameState CreateState(GameStates state)
        {
            switch (state)
            {
                case GameStates.Game:
                    return _gameStateFactory.Create();
                case GameStates.Win:
                    return _winStateFactory.Create();
                case GameStates.Loose:
                    return _looseStateFactory.Create();
                case GameStates.None:
                    break;
            }
            throw Assert.CreateException();
        } 
        #endregion
    }
}
