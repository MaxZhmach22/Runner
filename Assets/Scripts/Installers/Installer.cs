using UnityEngine;
using Zenject;

namespace Runner
{
    internal sealed class Installer : MonoInstaller
    {
        [Header("Prefabs")]
        [SerializeField] private Player _player;

        [Header("Ui Views")]
        [SerializeField] private Transform _placeForUi;
        //[SerializeField] private MainMenuView _mainMenuView;
        //[SerializeField] private GameUiView _gameUiView;
        //[SerializeField] private LooseMenuView _looseMenuView;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<Player>().FromInstance(_player).AsSingle();
            InstalGameStateFactories();
            //MainMenuControllerBindings();
            //LooseMenuControllerBindings();
            //GameUiControllerBindings();
            MainGameControllerBindings();
        }

        private void InstalGameStateFactories()
        {
            Container.Bind<GameStateFactory>().AsSingle();
            Container.BindFactory<StartGameState, StartGameState.Factory>().WhenInjectedInto<GameStateFactory>();
            Container.BindFactory<MainMenuController, MainMenuController.Factory>().WhenInjectedInto<StartGameState>();
            Container.BindFactory<GameGameState, GameGameState.Factory>().WhenInjectedInto<GameStateFactory>();
            Container.BindFactory<MainGameController, MainGameController.Factory>().WhenInjectedInto<GameGameState>();
            Container.BindFactory<EndGameState, EndGameState.Factory>().WhenInjectedInto<GameStateFactory>();
            Container.BindFactory<LooseGameController, LooseGameController.Factory>().WhenInjectedInto<EndGameState>();

        }

        //private void MainMenuControllerBindings()
        //{
        //    var mainMenuView = Container.InstantiatePrefabForComponent<MainMenuView>(
        //        _mainMenuView, _placeForUi);
        //    Container.Bind<MainMenuView>().FromInstance(mainMenuView).AsSingle();
        //}

        //private void LooseMenuControllerBindings()
        //{
        //   var looseMenuView = Container.InstantiatePrefabForComponent<LooseMenuView>(
        //       _looseMenuView, _placeForUi);
        //   Container.Bind<LooseMenuView>().FromInstance(looseMenuView).AsSingle();
        //}

        //private void GameUiControllerBindings()
        //{
        //    var gameUiView = Container.InstantiatePrefabForComponent<GameUiView>(
        //        _gameUiView, _placeForUi);
        //    Container.Bind<GameUiView>().FromInstance(gameUiView).AsSingle();
        //}

        private void MainGameControllerBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerMoveController>().AsSingle();
        }

    }
}