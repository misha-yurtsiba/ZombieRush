using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

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

    public TurretTile[] GetTuretTiles()
    {
        return turretTiles;
    }
}