using Zenject;
using UniRx;
using UnityEngine;

namespace PiggerBomber
{
    internal sealed class MainMenuController : BaseController
    {
        #region Fields

        private CompositeDisposable _disposables;
        private readonly Player _player; 

        #endregion


        #region ClassLifeCycles

        public MainMenuController(
            Player player)
        {
            _player = player;
            _disposables = new CompositeDisposable();
        }

        public override void Start()
        {
           
        }

        public override void Dispose()
        {
            _disposables.Clear();
        }

        #endregion


        public class Factory : PlaceholderFactory<MainMenuController>
        {
        }
    }
}