using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Rocket : MonoBehaviour, IPauseble
{
    [SerializeField] private ParticleSystem smokeParticle;
    [SerializeField] private ParticleSystem fireParticle;

    private Enemy targetEnemy;
    private ObjectPool<Rocket> rocketPool;
    private ObjectPool<Explosion> explosionPool;
    private IPause pauseGame;
    private Vector3 lastPos;
    private float speed;
    private float damage;
    private float blastRadius;

    private float ratio;
    private bool canMoving;

    [Inject]
    private void Construct(IPause pauseGame)
    {
        this.pauseGame = pauseGame;
    }
    public void Init(Enemy targetEnemy, ObjectPool<Rocket> rocketPool, ObjectPool<Explosion> explosionPool, float speed, float damage, float blastRadius)
    {
        this.rocketPool = rocketPool;
        this.explosionPool = explosionPool;
        this.targetEnemy = targetEnemy;
        this.speed = speed;
        this.damage = damage;
        this.blastRadius = blastRadius;

        lastPos = targetEnemy.transform.position;
        ratio = 0;
        canMoving = true;

        targetEnemy.enemyDestroyed += EnemyDestroyed;
    }
    private void OnEnable() => pauseGame.pause += IsGamePaused;
    private void OnDisable() => pauseGame.pause -= IsGamePaused;

    private void Update()
    {
        if (!canMoving) return;

        if (targetEnemy != null && targetEnemy.isSpavned)
            lastPos = targetEnemy.transform.position;

        if (Vector3.Distance(lastPos + new Vector3(0, 0.5f, 0), transform.position) < 0.1f)
            Explosion();

        transform.position = Vector3.MoveTowards(transform.position, lastPos + new Vector3(0, 0.5f, 0), speed * Time.deltaTime);

    }

    private void OnTriggerEnter(Collider other) => Explosion();

    private void Explosion()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);

        foreach (Collider collider in colliders)
            if (collider.TryGetComponent(out Enemy enemy))
                enemy.TakeDamage(damage);

        if(targetEnemy != null)
            targetEnemy.enemyDestroyed -= EnemyDestroyed;

        rocketPool.Relese(this);

        Explosion explosion = explosionPool.Get(transform.position);
        explosion.Init(explosionPool);
        explosion.PlayEfect();
    }

    private void EnemyDestroyed(Enemy enemy)
    {
        targetEnemy.enemyDestroyed -= EnemyDestroyed;       
        targetEnemy = null;
    }

    public void IsGamePaused(bool isGamePaused)
    {
        if (isGamePaused)
        {
            canMoving = false;
            smokeParticle.Pause();
            fireParticle.Pause();
        }
        else
        {
            canMoving = true;
            smokeParticle.Play();
            smokeParticle.Play();
        }
    }
}
