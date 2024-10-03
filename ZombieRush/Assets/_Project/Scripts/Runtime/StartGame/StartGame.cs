using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class StartGame : IStartGame
{
    private TurretTiles turretTiles;
    private EnemySpawner enemySpawner;
    private Money money;

    public StartGame(TurretTiles turretTiles, EnemySpawner enemySpawner, Money money)
    {
        this.turretTiles = turretTiles;
        this.enemySpawner = enemySpawner;
        this.money = money;
    }
    public void StartGameplay()
    {
        money.SetStartMoney(100);
        turretTiles.SetTurettTiles();
        enemySpawner.StartSpawn();
    }
}
