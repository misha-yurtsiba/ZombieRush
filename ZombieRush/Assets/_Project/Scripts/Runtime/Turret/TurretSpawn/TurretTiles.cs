using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTiles : MonoBehaviour
{
    private TurretTile[] turretTiles;

    public void SetTurettTiles()
    {
        turretTiles = GetComponentsInChildren<TurretTile>();

        for (int i = 0; i < turretTiles.Length; i++)
        {
            turretTiles[i].Init(i);
        }
    }
    public IEnumerable<TurretTile> GetTuretTiles()
    {
        return turretTiles;
    }
}