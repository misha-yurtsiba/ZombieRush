using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ExplosionFactory : GameObjectFactory<Explosion>
{
    public ExplosionFactory(DiContainer diContainer, Explosion objectPrefab) : base(diContainer, objectPrefab)
    {
    }

    public override Explosion Create(Vector3 spawnPos)
    {
        return diContainer.InstantiatePrefab(objectPrefab, spawnPos, Quaternion.identity, null).GetComponent<Explosion>();
    }
}
