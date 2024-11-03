using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveHandler
{
    private Money money;
    private PlayerHealth health;
    private TurretTiles turretTiles;
    private TurretSpawner turretSpawner;
    private WaveController waveController;

    private SaveSystem saveSystem;

    public SaveHandler(Money money, PlayerHealth health, TurretTiles turretTiles, TurretSpawner turretSpawner, WaveController waveController)
    {
        this.money = money;
        this.health = health;
        this.turretTiles = turretTiles;
        this.turretSpawner = turretSpawner;
        this.waveController = waveController;

        saveSystem = new SaveSystem();
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
        gameData.playerHealth = health.curentHealth;
        gameData.turretTileDatas = turretTileDatas;

        saveSystem.SaveData(gameData);
    }

    public void LoadGame()
    {
        GameData gameData = saveSystem.LoadData();

        turretSpawner.LoadTurrets(gameData.turretTileDatas);

    }
}
