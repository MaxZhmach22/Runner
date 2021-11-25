using System;
using UnityEngine;
using Zenject;

namespace Runner
{
    internal sealed class MainGameController : BaseController
    {
        #region Fields

        private readonly Player _player;
        private readonly PlayerMoveController _playerMoveController;

        #endregion

        #region ClassLifeCycles

        public MainGameController(
             Player player,
             PlayerMoveController playerMoveController)
        {
            _playerMoveController = playerMoveController;
            _player = player;
        }

        public override void Start()
        {
            _playerMoveController.Start();
        }

        public override void Dispose()
        {
            _playerMoveController.Dispose();
            Debug.Log(nameof(MainGameController) + " Disposed");
        }

        #endregion


        public sealed class Factory : PlaceholderFactory<MainGameController>
        {
        }
    }
}
