using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SingleTurret : Turret
{
    [SerializeField] protected Bullet bulletPrefab;

    [SerializeField] protected float attackRate;
    [SerializeField] protected float damage;

    protected ObjectPool<Bullet> bulletPool;


    [Inject]
    private void Construct(ObjectPool<Bullet> bulletPool)
    {
        this.bulletPool = bulletPool;
    }

    protected void Update()
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

    protected virtual void Shoot()
    {
        Bullet bullet = bulletPool.Get(bulletSpawnPoint.position);
        bullet.transform.rotation = bulletSpawnPoint.rotation;
        bullet.Init(targetEnemy.transform, bulletPool, 10, damage);

        attackTimer = 0;
        canAttack = false;
    }
}
