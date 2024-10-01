using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BulletFactory : GameObjectFactory<Bullet>
{
    public BulletFactory(DiContainer diContainer, Bullet objectPrefab) : base(diContainer, objectPrefab)
    {
    }

    public override Bullet Create(Vector3 spawnPos)
    {
        return diContainer.InstantiatePrefab(objectPrefab,spawnPos,Quaternion.identity,null).GetComponent<Bullet>();
    }
}
