using UnityEngine;
using Zenject;


namespace Runner
{
    internal sealed class Installer : MonoInstaller
    {
        #region Fields

        [Header("GameData")]
        [SerializeField] private GameData _gameData;

        [Header("Prefabs")]
        [SerializeField] private Player _player;

        [Header("Ui Views")]
        [SerializeField] private Transform _placeForUi;
        [SerializeField] private GameUiPresenter _gameUiPresenter;
        [SerializeField] private LooseUiPresenter _looseUiPresenter;
        [SerializeField] private WinUiPresenter _winUiPresenter;

        #endregion


        #region Methods

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<Player>().FromInstance(_player).AsSingle();
            Container.Bind<GameData>().FromInstance(_gameData).AsSingle();
            InstalGameStateFactories();
            MainGameControllerBindings();
            LooseGameControllerBindings();
            WinGameControllerBindings();
        }

        private void InstalGameStateFactories()
        {
            Container.Bind<GameStateFactory>().AsSingle();
            Container.BindFactory<GameGameState, GameGameState.Factory>().WhenInjectedInto<GameStateFactory>();
            Container.BindFactory<WinGameState, WinGameState.Factory>().WhenInjectedInto<GameStateFactory>();
            Container.BindFactory<LooseGameState, LooseGameState.Factory>().WhenInjectedInto<GameStateFactory>();
        }

        private void MainGameControllerBindings()
        {
            Container.Bind<MainGameController>().AsSingle();
            Container.BindInterfacesAndSelfTo<LevelController>().AsSingle();
            Container.Bind<ScoreController>().AsSingle();
            Container.Bind<LevelsInitializations>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerMoveController>().AsSingle();
            Container.BindInterfacesAndSelfTo<InputController>().AsSingle();

            var gameUiPresenter = Container.InstantiatePrefabForComponent<GameUiPresenter>(_gameUiPresenter, _placeForUi);
            Container.Bind<GameUiPresenter>().FromInstance(gameUiPresenter).AsSingle();
        }
        private void WinGameControllerBindings()
        {
            Container.BindInterfacesAndSelfTo<WinGameController>().AsSingle();
            var winUiPresenter = Container.InstantiatePrefabForComponent<WinUiPresenter>(_winUiPresenter, _placeForUi);
            Container.Bind<WinUiPresenter>().FromInstance(winUiPresenter).AsSingle();
        }

        private void LooseGameControllerBindings()
        {
            Container.BindInterfacesAndSelfTo<LooseGameController>().AsSingle();
            var looseUiPresenter = Container.InstantiatePrefabForComponent<LooseUiPresenter>(_looseUiPresenter, _placeForUi);
            Container.Bind<LooseUiPresenter>().FromInstance(looseUiPresenter).AsSingle();
        } 

        #endregion
    }
}