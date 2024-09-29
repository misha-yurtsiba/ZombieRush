using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemySpawner : MonoBehaviour 
{
    private IEnemyFactory enemyFactory;

    [SerializeField] private Transform spawnPosition;
    [SerializeField] private Transform targetPosition;

    [SerializeField] private float spawnRate;

    private float timer;

    [Inject]
    private void Construct(IEnemyFactory enemyFactory)
    {
        this.enemyFactory = enemyFactory;
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
        Enemy newEnemy = enemyFactory.CreateEnemy();
        Vector3 randomOffset = new Vector3(Random.Range(-2f,2f),0,0);
        
        newEnemy.transform.position = spawnPosition.position + randomOffset;
        Vector3 enemyTargetPos = targetPosition.position + randomOffset;

        newEnemy.Init(enemyTargetPos, 1);
    }
}
