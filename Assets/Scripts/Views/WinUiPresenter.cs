using UnityEngine;
using UnityEngine.UI;
using UniRx;


namespace Runner
{
    internal sealed class WinUiPresenter : BaseUiPresenter
    {
        #region Fields

        [SerializeField] private Button _nextLevelBtn;

        #endregion


        #region ClassLifeCycles


        protected override void Start()
        {
            base.Start();
            _nextLevelBtn.OnClickAsObservable().Subscribe(_ => Player.ChangeState(GameStates.Game)).AddTo(this);
        }

        #endregion
    }
}

