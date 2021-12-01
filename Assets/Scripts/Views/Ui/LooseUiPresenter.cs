using UnityEngine;
using UnityEngine.UI;
using UniRx;


namespace Runner
{
    internal sealed class LooseUiPresenter : BaseUiPresenter
    {
        #region Fields

        [SerializeField] private Button _retrytLevelBtn;

        #endregion


        #region ClassLifeCycles

        protected override void Start()
        {
            base.Start();
            _retrytLevelBtn.OnClickAsObservable().Subscribe(_ => Player.ChangeGameState(GameStates.Game)).AddTo(this);
        }

        #endregion
    }
}
