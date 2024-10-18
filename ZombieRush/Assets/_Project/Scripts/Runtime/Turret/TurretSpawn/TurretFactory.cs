using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class TurretFactory : ITurretFactory
{
    private DiContainer diContainer;
    private Turret turretPrefab;

    public TurretFactory(DiContainer diContainer, Turret turretPrefab)
    {
        this.diContainer = diContainer;
        this.turretPrefab = turretPrefab;
    }

    public Turret CreateTurret()
    {
        return diContainer.InstantiatePrefab(turretPrefab,Vector3.zero,Quaternion.identity,null).GetComponent<Turret>();
    }
}
