using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemySpawner : MonoBehaviour 
{
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private Transform targetPosition;
    [SerializeField] private float spawnRate;

    private ObjectPool<Enemy> enemyPool;
    private float timer;

    [Inject]
    private void Construct(ObjectPool<Enemy> enemyPool)
    {
        this.enemyPool = enemyPool;
    }

    public void StartSpawn()
    {
        StartCoroutine(SpawnCoroutine());
    }


    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            SpawnOneEnemy();
            yield return new WaitForSeconds(spawnRate);
        }
    }
    private void SpawnOneEnemy()
    {
        Vector3 randomOffset = new Vector3(Random.Range(-2f, 2f), 0, 0);
        Enemy newEnemy = enemyPool.Get(spawnPosition.position + randomOffset);
        Vector3 enemyTargetPos = targetPosition.position + randomOffset;

        newEnemy.Init(enemyTargetPos, 1,20,enemyPool);
    }
}
