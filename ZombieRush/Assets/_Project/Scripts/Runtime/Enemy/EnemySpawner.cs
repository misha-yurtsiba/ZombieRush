using System.Collections.Generic;
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

    private Coroutine coroutine;
    private ObjectPool<Enemy> enemyPool;
    private List<Enemy> enemyList = new List<Enemy>();
    private Money money;
    private IGameOver gameOver;
    private float startDelay;

    [Inject]
    private void Construct(Money money, IGameOver gameOver)
    {
        this.money = money;
        this.gameOver = gameOver;
    }

    private void OnEnable()
    {
        gameOver.gameOver += GameOver;
    }

    private void OnDisable()
    {
        gameOver.gameOver -= GameOver;
    }
    public void StartSpawn(SubWave newSubWave,int newStartDelay)
    {
        subWave = newSubWave;
        startDelay = newStartDelay;
        coroutine = StartCoroutine(SpawnCoroutine());
    }


    private IEnumerator SpawnCoroutine()
    {
        int spawnedEnemys = 0;
        yield return new WaitForSeconds(startDelay);
        while (spawnedEnemys != subWave.maxEnemyCount)
        {
            if (gameOver.isGameOver) yield break;

            SpawnOneEnemy();
            spawnedEnemys++;
            yield return new WaitForSeconds(subWave.spawnInterval);
        }
        endSpawnSubWave?.Invoke();
    }
    private void SpawnOneEnemy()
    {
        Vector3 randomOffset = new Vector3(UnityEngine.Random.Range(-1f, 1f), 0, 0);
        Enemy newEnemy = Instantiate(subWave.enemyPrefab, spawnPosition.position + randomOffset, Quaternion.Euler(0,180,0)).GetComponent<Enemy>();
        Vector3 enemyTargetPos = targetPosition.position + randomOffset;

        newEnemy.enemyDestroyed += DestoyEnemy;
        enemyList.Add(newEnemy);
        newEnemy.gameObject.SetActive(true);
        newEnemy.Init(enemyTargetPos,enemyPool, money);
    }

    private void GameOver()
    {
        StopCoroutine(coroutine);

        StartCoroutine(DestroyAllEnemyCoroutine());

    }

    private IEnumerator DestroyAllEnemyCoroutine()
    {
        yield return new WaitForSeconds(3);

        foreach (Enemy enemy in enemyList)
        {
            enemy.enemyDestroyed -= DestoyEnemy;
            Destroy(enemy.gameObject);
            yield return new WaitForSeconds(0.1f);
        }

        enemyList.Clear();
    }

    private void DestoyEnemy(Enemy enemy)
    {
        enemy.enemyDestroyed -= DestoyEnemy;
        enemyList.Remove(enemy);
        Destroy(enemy.gameObject);
    }
}
