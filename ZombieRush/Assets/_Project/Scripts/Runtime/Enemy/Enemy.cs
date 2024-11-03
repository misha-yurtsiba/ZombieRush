using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Enemy : MonoBehaviour
{

    [Header("Enemy stats")]
    [SerializeField] private int killMoney;
    [SerializeField] private float movingSpeed;
    [SerializeField] private float health;
    [SerializeField] private float damage;

    public Action<Enemy> enemyDestroyed;

    private Animator animator;
    private ObjectPool<Enemy> enemyPool;
    private Money money;
    private Vector3 targetPosition;

    private bool canMoving;

    public void Init(Vector3 targetPosition,ObjectPool<Enemy> enemyPool, Money money)
    {
        this.targetPosition = targetPosition;
        this.enemyPool = enemyPool;
        this.money = money;

        canMoving = true;

        if(animator == null)
            animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!canMoving) return;

        transform.position = Vector3.MoveTowards(transform.position,targetPosition,movingSpeed * Time.deltaTime);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            money.AddMoney(killMoney);
            enemyDestroyed?.Invoke(this);
        }
    }

    public void CanMoving(bool canMove)
    {
        canMoving = canMove;
        animator.speed = (canMove) ? 1 : 0;

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerHealth playerHealth))
        {
            playerHealth.TakeDamage(damage,transform.position);
            enemyDestroyed?.Invoke(this);
        }
    }
}
