using UnityEngine;
using System.Collections.Generic;
using Zenject;

public class GameplayInstaller : MonoInstaller
{
    [SerializeField] private TurretTiles turretTiles;
    [SerializeField] private GameUI gameUI;
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private WaveController waveController;
    [SerializeField] private WaveView waveView;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerHealthView playerHealthView;
    [SerializeField] private GameOverView gameOverView;

    [Header("Prefabs"),Space(2)]
    [SerializeField] private List<Turret> turrets;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Rocket rocketPrefab;
    [SerializeField] private Explosion explosionPrefab;
    [SerializeField] private ParticleSystem sparks;

    public override void InstallBindings()
    {
        BindTurretTiles();

        BindRestartGame();
        BindGameOverView();
        BindGameOwer();
        BindPauseGame();

        BindInputHandler();

        BindTurretsConfig();
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
        BindPlayerHealthView();
        BindPlayerHealth();

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

    private void BindRestartGame()
    {
        Container
            .BindInterfacesAndSelfTo<RestartGame>()
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
    private void BindTurretMover()
    {
        Container
            .BindInterfacesAndSelfTo<TurretMover>()
            .AsSingle()
            .WithArguments(sparks)
            .NonLazy();
    }

    private void BindMoney()
    {
        Container
            .BindInterfacesAndSelfTo<Money>()
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

    private void BindTurretsConfig()
    {
        TurretsConfig turretsConfig = new TurretsConfig();
        turretsConfig.turrets = turrets;

        Container
            .BindInstance(turretsConfig)
            .AsSingle()
            .NonLazy();
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
    private void BindPlayerHealthView()
    {
        Container
            .BindInstance(playerHealthView)
            .AsSingle()
            .Lazy();
    }

    private void BindPlayerHealth()
    {
        Container
            .BindInstance(playerHealth)
            .AsSingle()
            .Lazy();
    }
}
