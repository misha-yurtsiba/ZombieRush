using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Turret : MonoBehaviour
{
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private Transform turretObj;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private LayerMask enemyLayerMask;

    [SerializeField] private float attackRange;
    [SerializeField] private float attackRate;
    [SerializeField] private float damage;
    [SerializeField] private int turretPrice;

    private ObjectPool<Bullet> bulletPool;
    private Enemy targetEnemy;
    private float attackTimer;
    private int level;
    private bool canAttack;

    [Inject]
    private void Construct(ObjectPool<Bullet> bulletPool)
    {
        this.bulletPool = bulletPool;
    }
    private void Update()
    {
        if(attackTimer <= attackRate)
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

    }

    private bool IsEnemyFind()
    {
        Collider [] colliders = Physics.OverlapSphere(transform.position, attackRange,enemyLayerMask);
        if(colliders.Length > 0)
        {
            targetEnemy = colliders[0].GetComponent<Enemy>();
            return true;
        }
        targetEnemy = null;
        return false;
    }
    private bool IsEnemyInAttackRange()
    {
        if (targetEnemy == null || !targetEnemy.gameObject.activeInHierarchy)
            return false;

        if(Vector3.Distance(transform.position, targetEnemy.transform.position) > attackRange)
        {
            targetEnemy = null;
            return false;
        }

        turretObj.LookAt(targetEnemy.transform.position);
        return true;
    }

    private void Shoot()
    {
        Bullet bullet = bulletPool.Get(bulletSpawnPoint.position);
        bullet.transform.rotation = bulletSpawnPoint.rotation;
        bullet.Init(targetEnemy.transform,bulletPool, 10, damage);

        attackTimer = 0;
        canAttack = false;
    }
}
