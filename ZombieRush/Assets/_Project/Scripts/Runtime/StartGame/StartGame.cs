using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class StartGame : IStartGame
{
    private TurretTiles turretTiles;
    private WaveController waveController;
    private Money money;
    private InputHandler inputHandler;

    public StartGame(TurretTiles turretTiles, WaveController waveController, Money money, InputHandler inputHandler)
    {
        this.turretTiles = turretTiles;
        this.waveController = waveController;
        this.money = money;
        this.inputHandler = inputHandler;
    }
    public void StartGameplay()
    {
        money.SetStartMoney(100);
        turretTiles.SetTurettTiles();
        waveController.StartGame();
        inputHandler.Init();
    }
}
