using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class StartGame : IStartGame
{
    private TurretTiles turretTiles;
    private EnemySpawner enemySpawner;

    public StartGame(TurretTiles turretTiles, EnemySpawner enemySpawner)
    {
        this.turretTiles = turretTiles;
        this.enemySpawner = enemySpawner;
    }
    public void StartGameplay()
    {
        turretTiles.SetTurettTiles();
        enemySpawner.StartSpawn();
    }
}
