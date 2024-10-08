using UnityEngine;
using Zenject;

public class GameplayInstaller : MonoInstaller
{
    [SerializeField] private TurretTiles turretTiles;
    [SerializeField] private GameUI gameUI;
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private WaveController waveController;

    [Header("Prefabs"),Space(2)]
    [SerializeField] private Turret turretPrefab;
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private Bullet bulletPrefab;

    public override void InstallBindings()
    {
        BindTurretTiles();
        BindInputHandler();

        BindTurretPrefab();
        BindBulletPrefab();
        BindTurretFactory();
        BindBulletFactofy();
        BindBulletPool();
        BingTurretSpawner();

        BindMoney();
        BindEnemyPrefab();
        BindEnemyFactory();
        BindEnemyPool();
        BindEnemySpawner();
        BindWaveController();

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

    private void BindMoney()
    {
        Container
            .BindInterfacesAndSelfTo<Money>()
            .AsSingle()
            .Lazy();
    }
    private void BindTurretPrefab()
    {
        Container
            .BindInstance(turretPrefab)
            .AsSingle()
            .Lazy();
    }
    private void BindBulletPrefab()
    {
        Container
            .BindInstance(bulletPrefab)
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
    private void BindBulletPool()
    {
        Container
            .BindInterfacesAndSelfTo<ObjectPool<Bullet>>()
            .AsSingle()
            .WithArguments(5)
            .Lazy();
    }
    private void BingTurretSpawner()
    {
        Container
            .BindInterfacesAndSelfTo<TurretSpawner>()
            .AsSingle()
            .Lazy();
    }

    private void BindBulletFactofy()
    {
        Container
            .Bind<GameObjectFactory<Bullet>>()
            .To<BulletFactory>()
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
            .Bind<GameObjectFactory<Enemy>>()
            .To<EnemyFactory>()
            .AsSingle()
            .Lazy();
    }

    private void BindEnemyPool()
    {

            Container
            .BindInterfacesAndSelfTo<ObjectPool<Enemy>>()
            .AsSingle()
            .WithArguments(5)
            .Lazy();
    }

    private void BindWaveController()
    {
        Container
            .BindInstance(waveController)
            .AsSingle()
            .NonLazy();
    }
}
