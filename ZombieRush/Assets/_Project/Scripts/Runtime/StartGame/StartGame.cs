using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class StartGame : IStartGame
{
    private TurretTiles turretTiles;
    private WaveController waveController;
    private Money money;

    public StartGame(TurretTiles turretTiles, WaveController waveController, Money money)
    {
        this.turretTiles = turretTiles;
        this.waveController = waveController;
        this.money = money;
    }
    public void StartGameplay()
    {
        money.SetStartMoney(100);
        turretTiles.SetTurettTiles();
        waveController.StartGame();
    }
}
