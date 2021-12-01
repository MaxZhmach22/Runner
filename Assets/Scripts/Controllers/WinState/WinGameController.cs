using UnityEngine;


namespace Runner
{
    internal sealed class WinGameController : BaseController
    {
        #region Fields

        private readonly WinUiPresenter _winUiPresenter;
        private readonly LevelController _levelController;

        #endregion


        #region ClassLifeCycles

        public WinGameController(
            WinUiPresenter winUiPresenter,
            LevelController levelController)
        {
             _winUiPresenter = winUiPresenter;
            _levelController = levelController;
        }


        public override void Start()
        {
            _winUiPresenter.ShowMenu();
            SlowMotionEffect();
        }

        public override void Dispose()
        {
            _levelController.SwitchToNextLevel();
            _winUiPresenter.Dispose();
        }

        private void SlowMotionEffect()
        {
            Time.timeScale = 0.1f;
            Time.fixedDeltaTime = Time.timeScale * .02f;
        }

        #endregion
    }
}

