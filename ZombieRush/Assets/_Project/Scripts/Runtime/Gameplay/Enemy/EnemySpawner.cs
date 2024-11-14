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
    private Dictionary<int, ObjectPool<Enemy>> enemyPools;
    private List<Enemy> enemyList = new List<Enemy>();
    private Money money;
    private IGameOver gameOver;
    private IPause gamePause;
    private IRestart restartGame;

    private float startDelay;
    private bool canSpawn;

    [Inject]
    private void Construct(Money money, Dictionary<int, ObjectPool<Enemy>> enemyPools, IGameOver gameOver, IPause gamePause, IRestart restartGame)
    {
        this.money = money;
        this.enemyPools = enemyPools;
        this.gameOver = gameOver;
        this.gamePause = gamePause;
        this.restartGame = restartGame;
    }

    private void OnEnable()
    {
        gameOver.gameOver += GameOver;
        gamePause.pause += IsGamePaused;
    }

    private void OnDisable()
    {
        gameOver.gameOver -= GameOver;
        gamePause.pause -= IsGamePaused;
    }
    public void StartSpawn(SubWave newSubWave,int newStartDelay)
    {
        subWave = newSubWave;
        startDelay = newStartDelay;
        coroutine = StartCoroutine(SpawnCoroutine());
        canSpawn = true;
    }


    private IEnumerator SpawnCoroutine()
    {
        int spawnedEnemys = 0;
        yield return new WaitForSeconds(startDelay);
        while (spawnedEnemys != subWave.maxEnemyCount)
        {
            if (gameOver.isGameOver) yield break;

            if (!canSpawn)
            {
                yield return null;
            }
            else
            {
                SpawnOneEnemy();
                spawnedEnemys++;
                yield return new WaitForSeconds(subWave.spawnInterval);
            }
        }

        while (!canSpawn) yield return null;
        endSpawnSubWave?.Invoke();
    }
    private void SpawnOneEnemy()
    {

        int level = subWave.enemyPrefab.level;
        Vector3 randomOffset = new Vector3(UnityEngine.Random.Range(-1f, 1f), 0, 0);
        //Enemy newEnemy = Instantiate(subWave.enemyPrefab, spawnPosition.position + randomOffset, Quaternion.Euler(0,180,0)).GetComponent<Enemy>();
        
        Vector3 enemyTargetPos = targetPosition.position + randomOffset;
        Enemy newEnemy = enemyPools[level].Get(spawnPosition.position + randomOffset);

        newEnemy.transform.rotation = Quaternion.Euler(0, 180, 0);
        newEnemy.enemyDestroyed += DestoyEnemy;
        enemyList.Add(newEnemy);
        newEnemy.gameObject.SetActive(true);
        newEnemy.Init(enemyTargetPos,enemyPools[level], money);
    }

    private void GameOver()
    {
        StopCoroutine(coroutine);

        StartCoroutine(DestroyAllEnemyCoroutine());

    }

    public void DestroyAllEnemy()
    {
        StopCoroutine(coroutine);

        foreach (Enemy enemy in enemyList)
        {
            enemy.enemyDestroyed -= DestoyEnemy;
            Destroy(enemy.gameObject);
        }

        enemyList.Clear();
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
        enemyPools[enemy.level].Relese(enemy);
    }

    private void IsGamePaused(bool isPaused)
    {
        canSpawn = !isPaused;

        foreach (Enemy enemy in enemyList)
            enemy.CanMoving(!isPaused);
    }
}
