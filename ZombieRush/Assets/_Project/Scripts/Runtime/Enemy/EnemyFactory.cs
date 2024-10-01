using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class EnemyFactory : GameObjectFactory<Enemy>
{
    public EnemyFactory(DiContainer diContainer, Enemy enemyPrefab) : base(diContainer, enemyPrefab)
    {
        
    }
    public override Enemy Create(Vector3 spawnPos)
    {
        return diContainer.InstantiatePrefab(objectPrefab, spawnPos, Quaternion.identity,null).GetComponent<Enemy>();
    }
}
