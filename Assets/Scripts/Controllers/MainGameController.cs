using UnityEngine;
using Zenject;

namespace PiggerBomber
{
    internal sealed class MainGameController : BaseController
    {
        #region Fields

        private readonly Player _player;

        #endregion

        #region ClassLifeCycles

        public MainGameController(
             Player player)
        {
            _player = player;
        }

        public override void Start()
        {
            
        }

        public override void Dispose()
        {
            Debug.Log(nameof(MainGameController) + " Disposed");
        }

        #endregion


        public sealed class Factory : PlaceholderFactory<MainGameController>
        {
        }
    }
}
