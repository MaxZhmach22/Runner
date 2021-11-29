using System;
using UnityEngine;
using Zenject;

namespace Runner
{
    internal sealed class Installer : MonoInstaller
    {
        [Header("GameData")]
        [SerializeField] private GameData _gameData;

        [Header("Prefabs")]
        [SerializeField] private Player _player;

        [Header("Ui Views")]
        [SerializeField] private Transform _placeForUi;
        [SerializeField] private GameUiPresenter _gameUiPresenter;
        [SerializeField] private LooseUiPresenter _looseUiPresenter;
        [SerializeField] private WinUiPresenter _winUiPresenter;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<Player>().FromInstance(_player).AsSingle();
            Container.Bind<GameData>().FromInstance(_gameData).AsSingle();
            InstalGameStateFactories();
            //MainMenuControllerBindings();
            //LooseMenuControllerBindings();
            //GameUiControllerBindings();
            MainGameControllerBindings();
            LooseGameControllerBindings();
            WinGameControllerBindings();
        }

        private void WinGameControllerBindings()
        {
           var winUiPresenter = Container.InstantiatePrefabForComponent<WinUiPresenter>(_winUiPresenter, _placeForUi);
            Container.Bind<WinUiPresenter>().FromInstance(winUiPresenter).AsSingle();
        }

        private void LooseGameControllerBindings()
        {
            var looseUiPresenter = Container.InstantiatePrefabForComponent<LooseUiPresenter>(_looseUiPresenter, _placeForUi);
            Container.Bind<LooseUiPresenter>().FromInstance(looseUiPresenter).AsSingle();
        }

        private void InstalGameStateFactories()
        {
            Container.Bind<GameStateFactory>().AsSingle();
            Container.BindFactory<GameGameState, GameGameState.Factory>().WhenInjectedInto<GameStateFactory>();
            Container.BindFactory<MainGameController, MainGameController.Factory>().WhenInjectedInto<GameGameState>();
            Container.BindFactory<StartGameState, StartGameState.Factory>().WhenInjectedInto<GameStateFactory>();
            Container.BindFactory<MainMenuController, MainMenuController.Factory>().WhenInjectedInto<StartGameState>();
            Container.BindFactory<LooseGameState, LooseGameState.Factory>().WhenInjectedInto<GameStateFactory>();
            Container.BindFactory<LooseGameController, LooseGameController.Factory>().WhenInjectedInto<LooseGameState>();

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
            Container.BindInterfacesAndSelfTo<LevelController>().AsSingle();
            Container.Bind<ScoreController>().AsSingle();
            Container.Bind<LevelsInitializations>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerMoveController>().AsSingle();
            Container.BindInterfacesAndSelfTo<InputController>().AsSingle();

            var gameUiPresenter = Container.InstantiatePrefabForComponent<GameUiPresenter>(_gameUiPresenter, _placeForUi);
            Container.Bind<GameUiPresenter>().FromInstance(gameUiPresenter).AsSingle();
        }
    }
}