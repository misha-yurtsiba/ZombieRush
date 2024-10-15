using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class RocketFactory : GameObjectFactory<Rocket>
{
    public RocketFactory(DiContainer diContainer, Rocket objectPrefab) : base(diContainer, objectPrefab)
    {
    }
    public override Rocket Create(Vector3 spawnPos)
    {
        return diContainer.InstantiatePrefab(objectPrefab,spawnPos,Quaternion.identity,null).GetComponent<Rocket>();
    }
}
