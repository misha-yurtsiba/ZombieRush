using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Enemy : MonoBehaviour
{
    [Header("Enemy stats")]
    [SerializeField] private int killMoney;
    [SerializeField] private float movingSpeed;
    [SerializeField] private float health;

    private ObjectPool<Enemy> enemyPool;
    private Money money;
    private Vector3 targetPosition;

    public void Init(Vector3 targetPosition,ObjectPool<Enemy> enemyPool, Money money)
    {
        this.targetPosition = targetPosition;
        this.enemyPool = enemyPool;
        this.money = money;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position,targetPosition,movingSpeed * Time.deltaTime);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            money.AddMoney(killMoney);
            Destroy(gameObject);
        }
    }
}
