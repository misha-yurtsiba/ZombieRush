using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSpawner
{
    private InputHandler inputHandler;
    private TurretTiles turretTiles;
    private ITurretFactory turretFactory;
    private IEnumerable<TurretTile> emptyTurretTiles;

    private bool isSpawning = false;
    public TurretSpawner(InputHandler inputHandler, TurretTiles turretTiles, ITurretFactory turretFactory)
    {
        this.inputHandler = inputHandler;
        this.turretTiles = turretTiles;
        this.turretFactory = turretFactory;
    }

    public void BuyTurret()
    {
        if (isSpawning) return;

        inputHandler.playerTouched += SpawnTurret;
        emptyTurretTiles = turretTiles.GetTuretTiles();

        foreach(TurretTile tile in emptyTurretTiles)
            if(tile.curentTurret == null)
                tile.GetComponent<MeshRenderer>().material.color = Color.green;

        isSpawning = true;
    }

    private void SpawnTurret(Vector2 mousePosition)
    {
        Debug.Log(mousePosition);
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && hit.transform.TryGetComponent(out TurretTile turretTile) && turretTile.curentTurret == null)
        {
            Turret newTurret = turretFactory.CreateTurret();
            newTurret.transform.position = turretTile.transform.position + new Vector3(0,0.25f,0);
            turretTile.curentTurret = newTurret;
            turretTile.GetComponent<MeshRenderer>().material.color = Color.yellow;
        }

        foreach (TurretTile tile in emptyTurretTiles)
            if (tile.curentTurret == null)
                tile.GetComponent<MeshRenderer>().material.color = Color.yellow;

        inputHandler.playerTouched -= SpawnTurret;
        isSpawning = false;
    }
}
