using UnityEngine;
using UnityEngine.UI;
using UniRx;


namespace Runner
{
    internal sealed class GameUiPresenter : BaseUiPresenter
    {
        #region Fields

        [SerializeField] private Button _retrytLevelBtn;

        #endregion


        #region ClassLifeCycles

        protected override void Start()
        {
            base.Start();
            _retrytLevelBtn.OnClickAsObservable().Subscribe(_ => Player.ChangeState(GameStates.Game)).AddTo(this);
        }

        #endregion
    }
}

