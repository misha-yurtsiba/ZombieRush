using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int killMoney;

    private ObjectPool<Enemy> enemyPool;
    private Money money;
    private Vector3 targetPosition;

    private float movingSpeed;
    private float health = 20;
    public void Init(Vector3 targetPosition, float movingSpeed, float health,ObjectPool<Enemy> enemyPool, Money money)
    {
        this.targetPosition = targetPosition;
        this.movingSpeed = movingSpeed;  
        this.health = health;
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
            //enemyPool.Relese(this);
            Destroy(gameObject);
        }
    }
}
