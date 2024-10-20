using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTile : MonoBehaviour
{
    public Turret curentTurret;
    public int TuretTileId { get; private set; }
    
    public void Init(int id)
    {
        TuretTileId = id;
    }

    public bool TryGetTurret(out Turret turret)
    {
        turret = curentTurret;

        return (curentTurret == null) ? false : true;
    }
}
