using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SaveHandler : IDisposable
{
    private readonly Money money;
    private readonly PlayerHealth health;
    private readonly TurretTiles turretTiles;
    private readonly TurretSpawner turretSpawner;
    private readonly WaveController waveController;
    private readonly IGameOver gameOver;

    private SaveSystem saveSystem;

    public SaveHandler(Money money, PlayerHealth health, TurretTiles turretTiles, TurretSpawner turretSpawner, WaveController waveController, IGameOver gameOver)
    {
        this.money = money;
        this.health = health;
        this.turretTiles = turretTiles;
        this.turretSpawner = turretSpawner;
        this.waveController = waveController;
        this.gameOver = gameOver;

        saveSystem = new SaveSystem();

        waveController.nextWave += SaveGame;
        gameOver.gameOver += DeleteSave;
    }

    public void Dispose()
    {
        waveController.nextWave -= SaveGame;
        gameOver.gameOver -= DeleteSave;
    }

    public void SaveGame()
    {
        GameData gameData = new GameData();

        IEnumerable tiles = turretTiles.GetTuretTiles();
        List<TurretTileData> turretTileDatas = new List<TurretTileData>();   

        
        foreach(TurretTile turretTile in tiles)
        {
            if(turretTile.TryGetTurret(out Turret turret))
                turretTileDatas.Add(new TurretTileData(turretTile.TuretTileId, turret.level));
            else
                turretTileDatas.Add(new TurretTileData(turretTile.TuretTileId,0));
        }

        gameData.waveCount = waveController.waveCount;
        gameData.playerMoney = money.PlayerMoney;
        gameData.turretPrice = turretSpawner.turretPrice;
        gameData.playerHealth = health.curentHealth;
        gameData.turretTileDatas = turretTileDatas;

        saveSystem.SaveData(gameData);
    }

    public void LoadGame()
    {
        GameData gameData = saveSystem.LoadData();

        health.LoadHealth(gameData.playerHealth);
        waveController.LoadWave(gameData.waveCount);
        turretSpawner.LoadTurretPrice(gameData.turretPrice);
        money.SetStartMoney(gameData.playerMoney);
        turretSpawner.LoadTurrets(gameData.turretTileDatas);

    }

    public bool IsSaveExist()
    {
        return saveSystem.IsFileExist();
    } 

    public void DeleteSave() => saveSystem.DeleteSaveFile();
}
