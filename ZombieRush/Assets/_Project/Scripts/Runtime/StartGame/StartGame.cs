using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class StartGame : IStartGame
{
    private TurretTiles turretTiles;
    private WaveController waveController;
    private EnemySpawner enemySpawner;
    private Money money;
    private InputHandler inputHandler;
    private GameOver gameOver;
    private PlayerHealth playerHealth;


    public StartGame(
        TurretTiles turretTiles,
        WaveController waveController,
        Money money, 
        InputHandler inputHandler,
        EnemySpawner enemySpawner,
        GameOver gameOver,
        PlayerHealth playerHealth)
    {
        this.turretTiles = turretTiles;
        this.waveController = waveController;
        this.money = money;
        this.inputHandler = inputHandler;
        this.enemySpawner = enemySpawner;
        this.gameOver = gameOver;
        this.playerHealth = playerHealth;
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


}
