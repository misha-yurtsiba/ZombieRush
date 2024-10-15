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

    [Inject]
    private void Construct(ObjectPool<Rocket> rocketPool)
    {
        this.rocketPool = rocketPool;
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

        if ((IsEnemyInAttackRange() || IsEnemyFind()) && canAttack)
        {
            Shoot();
        }

        base.Update();
    }

    private void Shoot()
    {
        Rocket rocket = rocketPool.Get(bulletSpawnPoint.position);
        rocket.transform.rotation = bulletSpawnPoint.rotation;
        rocket.Init(targetEnemy.transform, rocketPool, 10, damage, blastRadius);

        attackTimer = 0;
        canAttack = false;
    }
}
