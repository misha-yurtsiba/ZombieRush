using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class StartGame : IStartGame
{
    private readonly TurretTiles turretTiles;
    private readonly WaveController waveController;
    private readonly EnemySpawner enemySpawner;
    private readonly TurretSpawner turretSpawner;
    private readonly Money money;
    private readonly InputHandler inputHandler;
    private readonly GameOver gameOver;
    private readonly PlayerHealth playerHealth;
    private readonly SaveHandler saveHandler;

    public StartGame(
        TurretTiles turretTiles,
        WaveController waveController,
        Money money, 
        InputHandler inputHandler,
        EnemySpawner enemySpawner,
        TurretSpawner turretSpawner,
        GameOver gameOver,
        PlayerHealth playerHealth,
        SaveHandler saveHandler)
    {
        this.turretTiles = turretTiles;
        this.waveController = waveController;
        this.money = money;
        this.inputHandler = inputHandler;
        this.enemySpawner = enemySpawner;
        this.gameOver = gameOver;
        this.playerHealth = playerHealth;
        this.turretSpawner = turretSpawner;
        this.saveHandler = saveHandler;
    }

    public void StartGameplay()
    {
        money.SetStartMoney(100);
        turretTiles.SetTurettTiles();
        waveController.StartGame();
        inputHandler.Init();
        gameOver.Init();
        playerHealth.Init();
    }

    public void ContinueGame()
    {
        turretTiles.SetTurettTiles();
        inputHandler.Init();
        gameOver.Init();
        saveHandler.LoadGame();
    }

    public void ExitGame()
    {
        turretSpawner.RemoveAllTurret();
        enemySpawner.DestroyAllEnemy();
    }
}
