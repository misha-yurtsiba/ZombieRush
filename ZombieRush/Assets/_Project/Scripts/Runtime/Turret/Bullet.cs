using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform targetPos;
    private ObjectPool<Bullet> bulletPool;
    private float speed;
    private float damage;

    public void Init(Transform targetPos, ObjectPool<Bullet> bulletPool, float speed, float damage)
    {
        this.targetPos = targetPos; 
        this.bulletPool = bulletPool;
        this.speed = speed;
        this.damage = damage;
    }

    private void Update()
    {
        if (!targetPos.gameObject.activeInHierarchy)
        {
            GetComponent<TrailRenderer>().Clear();
            bulletPool.Relese(this);
        }
        else
            transform.position = Vector3.MoveTowards(transform.position, targetPos.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(damage);
            GetComponent<TrailRenderer>().Clear();
            bulletPool.Relese(this);
        }
    }
}