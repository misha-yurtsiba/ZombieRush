using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class RocketTurret : Turret
{
    [SerializeField] private Rocket rocketPrefab;

    [SerializeField] private float attackRate;
    [SerializeField] private float damage;
    [SerializeField] private float blastRadius;

    private ObjectPool<Rocket> rocketPool;
    private ObjectPool<Explosion> explosionPool;

    [Inject]
    private void Construct(ObjectPool<Rocket> rocketPool, ObjectPool<Explosion> explosionPool)
    {
        this.rocketPool = rocketPool;
        this.explosionPool = explosionPool;
    }

    private void Update()
    {
        if (attackTimer <= attackRate)
        {
            canAttack = false;
            attackTimer += Time.deltaTime;
        }
        else
            canAttack = true;

        if (!isMoving && (IsEnemyInAttackRange() || IsEnemyFind()) && canAttack)
        {
            Shoot();
        }

        base.Update();
    }

    private void Shoot()
    {
        Rocket rocket = rocketPool.Get(bulletSpawnPoint.position);
        //rocket.transform.rotation = bulletSpawnPoint.rotation;
        rocket.transform.LookAt(targetEnemy.transform);
        rocket.Init(targetEnemy.transform, rocketPool,explosionPool, 10, damage, blastRadius);

        attackTimer = 0;
        canAttack = false;
    }
}
