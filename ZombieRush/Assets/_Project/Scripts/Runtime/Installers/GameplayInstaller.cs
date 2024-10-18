using UnityEngine;
using Zenject;

public class GameplayInstaller : MonoInstaller
{
    [SerializeField] private TurretTiles turretTiles;
    [SerializeField] private GameUI gameUI;
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private WaveController waveController;
    [SerializeField] private WaveView waveView;

    [Header("Prefabs"),Space(2)]
    [SerializeField] private Turret turretPrefab;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Rocket rocketPrefab;
    [SerializeField] private Explosion explosionPrefab;

    public override void InstallBindings()
    {
        BindTurretTiles();
        BindInputHandler();

        BindTurretPrefab();
        BindBulletPrefab();
        BindRocketPrefab();
        BindExplosionPrefab();

        BindTurretFactory();
        BindBulletFactofy();
        BindRocketFactofy();
        BindExplosionFactofy();

        BindBulletPool();
        BindRocketPool();
        BindExplosionPool();
        BingTurretSpawner();
        BindTurretMover();

        BindMoney();
        //BindEnemyFactory();
        //BindEnemyPool();
        BindEnemySpawner();

        BindWaveController();
        BindWaveView();

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
    private void BindTurretMover()
    {
        Container
            .BindInterfacesAndSelfTo<TurretMover>()
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
    private void BindRocketPrefab()
    {
        Container
            .BindInstance(rocketPrefab)
            .AsSingle()
            .Lazy();
    }
    private void BindExplosionPrefab()
    {
        Container
            .BindInstance(explosionPrefab)
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

    private void BindRocketPool()
    {
        Container
            .BindInterfacesAndSelfTo<ObjectPool<Rocket>>()
            .AsSingle()
            .WithArguments(3)
            .Lazy();
    }

    private void BindExplosionPool()
    {
        Container
            .BindInterfacesAndSelfTo<ObjectPool<Explosion>>()
            .AsSingle()
            .WithArguments(3)
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
    private void BindRocketFactofy()
    {
        Container
            .Bind<GameObjectFactory<Rocket>>()
            .To<RocketFactory>()
            .AsSingle()
            .Lazy();
    }
    private void BindExplosionFactofy()
    {
        Container
            .Bind<GameObjectFactory<Explosion>>()
            .To<ExplosionFactory>()
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
    private void BindWaveView()
    {
        Container
            .BindInstance(waveView)
            .AsSingle()
            .NonLazy();
    }
}
