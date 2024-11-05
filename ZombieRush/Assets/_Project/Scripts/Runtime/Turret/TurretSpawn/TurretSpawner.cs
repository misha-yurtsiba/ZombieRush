using System;
using System.Collections.Generic;
using UnityEngine;

public class TurretSpawner : IDisposable
{
    public event Action<int> changeTurretPrice;

    public int turretStartPrice = 100;
    public int turretPrice;

    

    private InputHandler inputHandler;
    private TurretTiles turretTiles;
    private Money money;

    private List<Turret> turretsList = new List<Turret>();
    private ITurretFactory turretFactory;
    private IGameOver gameOver;
    private IRestart restartGame;
    private IEnumerable<TurretTile> emptyTurretTiles;

    public bool isSpawning { get; private set; }
    public TurretSpawner(InputHandler inputHandler, TurretTiles turretTiles, ITurretFactory turretFactory, IGameOver gameOver, IRestart restartGame, Money money)
    {
        this.inputHandler = inputHandler;
        this.turretTiles = turretTiles;
        this.turretFactory = turretFactory;
        this.money = money;
        this.gameOver = gameOver;
        this.restartGame = restartGame;

        isSpawning = false;
        turretPrice = turretStartPrice;

        gameOver.gameOver += RemoveAllTurret;
        restartGame.restart += ResetTurretPrice;
    }

    public void Dispose()
    {
        gameOver.gameOver -= RemoveAllTurret;
        restartGame.restart -= ResetTurretPrice;
    }

    public void BuyTurret()
    {
        if (isSpawning || money.PlayerMoney < turretPrice) return;

        inputHandler.playerTouched += SpawnTurret;
        emptyTurretTiles = turretTiles.GetTuretTiles();

        foreach (TurretTile tile in emptyTurretTiles)
            if (tile.curentTurret == null)
                tile.SetActiveBlinking(true);

        isSpawning = true;
    }

    private void SpawnTurret(Vector2 mousePosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && hit.transform.TryGetComponent(out TurretTile turretTile) && turretTile.curentTurret == null)
        {
            Turret newTurret = GetOneTurret(1, turretTile);
            money.Buy(turretPrice);

            turretPrice += (int)Mathf.Round(turretPrice * 0.1f / 10) * 10;
            changeTurretPrice?.Invoke(turretPrice);
        }

        foreach (TurretTile tile in emptyTurretTiles)
            tile.SetActiveBlinking(false);

        inputHandler.playerTouched -= SpawnTurret;
        isSpawning = false;
    }

    public Turret GetOneTurret(int level, TurretTile turretTile)
    {
        Turret newTurret = turretFactory.CreateTurret(level);
        newTurret.transform.position = turretTile.transform.position + new Vector3(0, 0, 0);
        turretTile.curentTurret = newTurret;
        turretsList.Add(newTurret);

        return newTurret;
    }

    public void RemoveTurret(Turret turret)
    {
        turretsList.Remove(turret);
        UnityEngine.Object.Destroy(turret.gameObject);
    }

    public void RemoveAllTurret()
    {
        foreach (Turret turret in turretsList)
            UnityEngine.Object.Destroy(turret.gameObject);

        turretsList.Clear();
    }

    public void LoadTurrets(List<TurretTileData> turretTileDatas)
    {
        TurretTile[] tiles = turretTiles.GetTuretTiles();

        foreach (TurretTileData tileData in turretTileDatas)
        {
            if (tileData.turretLevel == 0) continue;

            Turret newTurret = GetOneTurret(tileData.turretLevel, tiles[tileData.turretTileId]);
        }

    }

    private void ResetTurretPrice()
    {
        turretPrice = turretStartPrice;
        changeTurretPrice?.Invoke(turretPrice);
    }
}
