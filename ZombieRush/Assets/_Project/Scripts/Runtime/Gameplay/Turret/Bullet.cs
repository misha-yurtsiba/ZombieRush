using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Bullet : MonoBehaviour, IPauseble
{
    private Enemy targetEnemy;
    private ObjectPool<Bullet> bulletPool;
    private TrailRenderer trailRenderer;
    private IPause pauseGame;
    private float speed;
    private float damage;
    private bool canMoving;


    [Inject]
    private void Construct(IPause pauseGame)
    {
        this.pauseGame = pauseGame;

        trailRenderer = GetComponent<TrailRenderer>();
    }
    private void OnEnable() => pauseGame.pause += IsGamePaused;
    private void OnDisable() => pauseGame.pause -= IsGamePaused;
    public void Init(Enemy targetEnemy, ObjectPool<Bullet> bulletPool, float speed, float damage)
    {
        this.targetEnemy = targetEnemy; 
        this.bulletPool = bulletPool;
        this.speed = speed;
        this.damage = damage;

        canMoving = true;
    }

    private void Update()
    {
        if (!canMoving) return;

        if (!targetEnemy.isSpavned)
        {
            GetComponent<TrailRenderer>().Clear();
            bulletPool.Relese(this);
        }
        else
            transform.position = Vector3.MoveTowards(transform.position, targetEnemy.transform.position + new Vector3(0,0.5f,0), speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(damage);
            trailRenderer.Clear();
            bulletPool.Relese(this);
        }
    }

    public void IsGamePaused(bool isGamePaused)
    {
        if (isGamePaused)
        {
            canMoving = false;

        }
        else
        {
            canMoving = true;

        }
    }
}
