using UnityEngine;
using Zenject;

public class GameplayInstaller : MonoInstaller
{
    [SerializeField] private TurretTiles turretTiles;
    [SerializeField] private GameUI gameUI;
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private Turret turretPrefab;
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private EnemySpawner enemySpawner;

    public override void InstallBindings()
    {
        BindTurretTiles();
        BindInputHandler();

        BindTurretPrefab();
        BindTurretFactory();
        BingTurretSpawner();

        BindEnemyPrefab();
        BindEnemyFactory();
        BindEnemySpawner();

        BindGameUI();
        BindStartGame();
    }

    private void BindStartGame()
    {
        Container
            .BindInterfacesAndSelfTo<StartGame>()
            .AsSingle()
            .NonLazy();
    }

    private void BindTurretTiles()
    {
        Container
            .BindInstance(turretTiles)
            .AsSingle()
            .NonLazy();
    }
    private void BindGameUI()
    {
        Container
            .BindInstance(gameUI)
            .AsSingle()
            .NonLazy();
    }

    private void BindInputHandler()
    {
        Container
            .BindInstance(inputHandler)
            .AsSingle()
            .NonLazy();
    }
    private void BindTurretPrefab()
    {
        Container
            .BindInstance(turretPrefab)
            .AsSingle()
            .Lazy();
    }
    private void BindTurretFactory()
    {
        Container
            .BindInterfacesAndSelfTo<TurretFactory>()
            .AsSingle()
            .NonLazy();
    }

    private void BingTurretSpawner()
    {
        Container
            .BindInterfacesAndSelfTo<TurretSpawner>()
            .AsSingle()
            .Lazy();
    }

    private void BindEnemyPrefab()
    {
        Container
            .BindInstance(enemyPrefab)
            .AsSingle()
            .Lazy();
    }
    private void BindEnemySpawner()
    {
        Container
            .BindInstance(enemySpawner)
            .AsSingle()
            .Lazy();
    }
    private void BindEnemyFactory()
    {
        Container
            .BindInterfacesAndSelfTo<EnemyFactory>()
            .AsSingle()
            .Lazy();
    }

}
