using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoudleTurret : SingleTurret
{
    [SerializeField] private Transform[] muzzles;
    private int gunIndex;
    private void Start()
    {
        gunIndex = 1;
        bulletSpawnPoint = muzzles[0]; ;
    }

    protected override void Shoot()
    {
        base.Shoot();
        gunIndex *= -1;
        bulletSpawnPoint = (gunIndex == 1) ? muzzles[0] : muzzles[1];
    }
}
