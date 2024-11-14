using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class TurretFactory : ITurretFactory
{
    private DiContainer diContainer;
    private TurretsConfig turrets;

    private Dictionary<int, Turret> turretsDict = new Dictionary<int, Turret>();

    public TurretFactory(DiContainer diContainer, TurretsConfig turrets)
    {
        this.diContainer = diContainer;
        this.turrets = turrets;

        Debug.Log(turrets);
        foreach(Turret turret in this.turrets.turrets)
            turretsDict.Add(turret.level, turret);
    }

    public Turret CreateTurret(int level)
    {
        return diContainer.InstantiatePrefab(turretsDict[level],Vector3.zero,Quaternion.identity,null).GetComponent<Turret>();
    }
}
