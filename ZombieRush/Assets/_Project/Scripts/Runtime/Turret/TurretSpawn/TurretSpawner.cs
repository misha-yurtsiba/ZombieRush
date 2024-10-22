using System;
using System.Collections.Generic;
using UnityEngine;

public class TurretSpawner
{
    public Action<int> turretSpawned;

    public int turretPrice = 100;

    private InputHandler inputHandler;
    private TurretTiles turretTiles;
    private Money money;
    private ITurretFactory turretFactory;
    private IEnumerable<TurretTile> emptyTurretTiles;

    public bool isSpawning { get; private set; }
    public TurretSpawner(InputHandler inputHandler, TurretTiles turretTiles, ITurretFactory turretFactory, Money money)
    {
        this.inputHandler = inputHandler;
        this.turretTiles = turretTiles;
        this.turretFactory = turretFactory;
        this.money = money;

        isSpawning = false;
    }

    public void BuyTurret()
    {
        if (isSpawning || money.PlayerMoney < turretPrice) return;

        inputHandler.playerTouched += SpawnTurret;
        emptyTurretTiles = turretTiles.GetTuretTiles();

        foreach(TurretTile tile in emptyTurretTiles)
            if(tile.curentTurret == null)
                //tile.GetComponent<MeshRenderer>().material.color = Color.green;

        isSpawning = true;
    }

    private void SpawnTurret(Vector2 mousePosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && hit.transform.TryGetComponent(out TurretTile turretTile) && turretTile.curentTurret == null)
        {
            Turret newTurret = turretFactory.CreateTurret(1);
            newTurret.transform.position = turretTile.transform.position + new Vector3(0,0,0);
            turretTile.curentTurret = newTurret;
            //turretTile.GetComponent<MeshRenderer>().material.color = Color.yellow;
            money.Buy(turretPrice);

            //turretPrice += (int)Mathf.Round(turretPrice * 0.1f / 10) * 10;
            turretSpawned?.Invoke(turretPrice);
        }

        foreach (TurretTile tile in emptyTurretTiles)
            if (tile.curentTurret == null)
                //tile.GetComponent<MeshRenderer>().material.color = Color.yellow;

        inputHandler.playerTouched -= SpawnTurret;
        isSpawning = false;
    }
}
