using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameStateInstaller : MonoInstaller
{
    [SerializeField] private GameOverView gameOverView;

    public override void InstallBindings()
    {
        BindRestartGame();

        BindGameOverView();

        BindGameOwer();

        BindPauseGame();
    }

    private void BindRestartGame()
    {
        Container
            .BindInterfacesAndSelfTo<RestartGame>()
            .AsSingle()
            .Lazy();
    }
    private void BindGameOverView()
    {
        Container
            .BindInstance(gameOverView)
            .AsSingle()
            .Lazy();
    }

    private void BindGameOwer()
    {
        Container
            .BindInterfacesAndSelfTo<GameOver>()
            .AsSingle()
            .Lazy();
    }

    private void BindPauseGame()
    {
        Container
            .BindInterfacesAndSelfTo<PauseGame>()
            .AsSingle()
            .Lazy();
    }
}
