using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class EnemyFactory : IEnemyFactory
{
    private DiContainer diContainer;
    private Enemy enemyPrefab;

    public EnemyFactory(DiContainer diContainer, Enemy enemyPrefab)
    {
        this.diContainer = diContainer;
        this.enemyPrefab = enemyPrefab;
    }
    
    public Enemy CreateEnemy()
    {
        return diContainer.InstantiatePrefab(enemyPrefab,Vector3.zero,Quaternion.identity,null).GetComponent<Enemy>();
    }
}
