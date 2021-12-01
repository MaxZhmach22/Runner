﻿using UnityEngine;


namespace Runner
{
    internal sealed class LooseGameController : BaseController
    {
        #region Fields

        private readonly LooseUiPresenter _looseUiPresenter;
        private readonly Player _player;

        #endregion


        #region ClassLifeCycles

        public LooseGameController(
            LooseUiPresenter looseUiPresenter,
            Player player)
        {
            _looseUiPresenter = looseUiPresenter;
            _player = player;
        }
             
        public override void Start()
        {
            _looseUiPresenter.ShowMenu();
            SlowMotionEffect();
        }

        public override void Dispose()
        {
            _looseUiPresenter.Dispose();
            _player.ResetValues();
        }

        private static void SlowMotionEffect()
        {
            Time.timeScale = 0.1f;
            Time.fixedDeltaTime = Time.timeScale * .02f;
        }

        #endregion
    }
}

