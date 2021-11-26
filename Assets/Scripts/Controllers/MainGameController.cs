using UnityEngine;
using Zenject;


namespace Runner
{
    internal sealed class MainGameController : BaseController
    {
        #region Fields

        private readonly Player _player;
        private readonly LevelController _levelController;
        private readonly InputController _inputController;
        private readonly PlayerMoveController _playerMoveController;

        #endregion

        #region ClassLifeCycles

        public MainGameController(
             Player player,
             LevelController levelController,
             InputController inputController,
             PlayerMoveController playerMoveController)
        {
            _player = player;
            _levelController = levelController;
            _inputController = inputController;
            _playerMoveController = playerMoveController;
        }

        public override void Start()
        {
            _player.gameObject.SetActive(true);
            _levelController.Start();
            _inputController.Start();
            _playerMoveController.Start();
        }

        public override void Dispose()
        {
            _levelController.Dispose();
            _playerMoveController.Dispose();
            _inputController.Dispose();
            Debug.Log(nameof(MainGameController) + " Disposed");
        }

        #endregion


        public sealed class Factory : PlaceholderFactory<MainGameController>
        {
        }
    }
}
