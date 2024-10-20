using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class Turret : MonoBehaviour
{
    public bool isMoving;

    [SerializeField] protected Transform bulletSpawnPoint;
    [SerializeField] protected Transform turretObj;
    [SerializeField] protected LayerMask enemyLayerMask;

    [SerializeField] protected float attackRange;
    [SerializeField] protected float rotationSpead;
    [SerializeField] protected int turretPrice;

    protected Enemy targetEnemy;

    protected float attackTimer;
    protected bool canAttack;

    private Quaternion targetRotation;
    private float rotateTimer;
    [field:SerializeField] public int level { get; private set; }
    protected void Update()
    {
        RotateTurret();
    }
    protected bool IsEnemyFind()
    {
        Collider [] colliders = Physics.OverlapSphere(transform.position, attackRange,enemyLayerMask);
        if(colliders.Length > 0)
        {
            int index = 0;
            float minDistance = Vector3.Distance(transform.position, colliders[0].transform.position); 

            for(int i = 0; i < colliders.Length; i++)
            {
                float distance = Vector3.Distance(transform.position, colliders[i].transform.position);

                if(distance < minDistance)
                {
                    minDistance = distance;
                    index = i;
                }
            }
            targetEnemy = colliders[index].GetComponent<Enemy>();
            return true;
        }
        targetEnemy = null;
        return false;
    }
    protected bool IsEnemyInAttackRange()
    {
        if (targetEnemy == null || !targetEnemy.gameObject.activeInHierarchy)
        {
            rotateTimer = 0;
            canAttack = false;
            return false;
        }

        if(Vector3.Distance(transform.position, targetEnemy.transform.position) > attackRange)
        {
            rotateTimer = 0;
            canAttack = false;
            targetEnemy = null;
            return false;
        }
        return true;
    }

    private void RotateTurret()
    {
        if (targetEnemy == null) return;

        Vector3 dir = targetEnemy.transform.position - turretObj.position;

        targetRotation = Quaternion.LookRotation(dir);
        turretObj.transform.rotation = Quaternion.Slerp(turretObj.transform.rotation, targetRotation, rotationSpead * Time.deltaTime);

        if (rotateTimer < 0.5)
            rotateTimer += Time.deltaTime;
        else
            canAttack = true;
    }
}
