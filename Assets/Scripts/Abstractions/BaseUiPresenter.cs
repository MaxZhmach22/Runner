using UniRx;
using Zenject;
using TMPro;
using UnityEngine;


namespace Runner
{
    internal abstract class BaseUiPresenter : MonoBehaviour
    {
        #region Fields

        [SerializeField] protected TMP_Text ScoreTxt;
        protected Player Player;
        private ScoreController _scoreController;

        #endregion


        #region ClassLifeCycles

        [Inject]
        protected void Init(
            Player player, 
            ScoreController scoreController)
        {
            Player = player;
            _scoreController = scoreController;
        }

        protected virtual void Start()
        {
            HideMenu();
            _scoreController.OnScoreChange.Subscribe(score => ScoreTxt.text = score.ToString()).AddTo(this);
        }

        public virtual void Dispose() =>
            HideMenu();

        #endregion


        #region Methods

        public virtual void ShowMenu() =>
            gameObject.SetActive(true);

        protected virtual void HideMenu() =>
            gameObject.SetActive(false); 

        #endregion
    }
}

