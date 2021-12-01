using UnityEngine;


namespace Runner
{
    internal sealed class MainGameController : BaseController
    {
        #region Fields

        private readonly Player _player;
        private readonly LevelController _levelController;
        private readonly InputController _inputController;
        private readonly PlayerMoveController _playerMoveController;
        private readonly ScoreController _scoreController;
        private readonly GameUiPresenter _gameUiPresenter;

        #endregion

        #region ClassLifeCycles

        public MainGameController(
             Player player,
             LevelController levelController,
             InputController inputController,
             PlayerMoveController playerMoveController,
             ScoreController scoreController,
             GameUiPresenter gameUiPresenter)
        {
            _player = player;
            _levelController = levelController;
            _inputController = inputController;
            _playerMoveController = playerMoveController;
            _scoreController = scoreController;
            _gameUiPresenter = gameUiPresenter;
        }

        public override void Start()
        {
            DeafultTimeScale();
            _player.gameObject.SetActive(true);
            _levelController.Start();
            _inputController.Start();
            _playerMoveController.Start();
            _scoreController.Start();
            _gameUiPresenter.ShowMenu();
        }

        public override void Dispose()
        {
            _levelController.Dispose();
            _playerMoveController.Dispose();
            _inputController.Dispose();
            _scoreController.Dispose();
            _gameUiPresenter.Dispose();
            Debug.Log(nameof(MainGameController) + " Disposed");
        }

        private void DeafultTimeScale()
        {
            Time.timeScale = 1f;
            Time.fixedDeltaTime = Time.timeScale * .02f;
        }

        #endregion
    }
}
