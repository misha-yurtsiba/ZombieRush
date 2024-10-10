using System.Collections;
using System;
using UnityEngine;
using Zenject;

public class EnemySpawner : MonoBehaviour 
{
    public Action endSpawnSubWave;
    public SubWave subWave;

    [SerializeField] private Transform spawnPosition;
    [SerializeField] private Transform targetPosition;

    private Money money;
    private ObjectPool<Enemy> enemyPool;
    private float startDelay;

    [Inject]
    private void Construct(Money money)
    {
        this.money = money;
    }

    public void StartSpawn(SubWave newSubWave,int newStartDelay)
    {
        subWave = newSubWave;
        startDelay = newStartDelay;
        StartCoroutine(SpawnCoroutine());
    }


    private IEnumerator SpawnCoroutine()
    {
        int spawnedEnemys = 0;
        yield return new WaitForSeconds(startDelay);
        while (spawnedEnemys != subWave.maxEnemyCount)
        {
            SpawnOneEnemy();
            spawnedEnemys++;
            yield return new WaitForSeconds(subWave.spawnInterval);
        }
        endSpawnSubWave?.Invoke();
    }
    private void SpawnOneEnemy()
    {
        Vector3 randomOffset = new Vector3(UnityEngine.Random.Range(-2f, 2f), 0, 0);
        Enemy newEnemy = Instantiate(subWave.enemyPrefab, spawnPosition.position + randomOffset, Quaternion.Euler(0,180,0)).GetComponent<Enemy>();
        Vector3 enemyTargetPos = targetPosition.position + randomOffset;

        newEnemy.gameObject.SetActive(true);
        newEnemy.Init(enemyTargetPos,enemyPool, money);
    }
}
