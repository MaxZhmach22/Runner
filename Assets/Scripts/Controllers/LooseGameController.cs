namespace Runner
{
    internal sealed class LooseGameController : BaseController
    {
        #region Fields

        private readonly LooseUiPresenter _looseUiPresenter;

        #endregion


        #region ClassLifeCycles

        public LooseGameController(
            LooseUiPresenter looseUiPresenter)
        {
            _looseUiPresenter = looseUiPresenter;
        }
             
        public override void Start()
        {
            _looseUiPresenter.ShowMenu();
            TimeShift.SlowMotionEffect();
        }

        public override void Dispose()
        {
            _looseUiPresenter.Dispose();
        }

        #endregion
    }
}

