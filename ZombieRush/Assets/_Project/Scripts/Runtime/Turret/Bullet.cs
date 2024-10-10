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
        if (targetPos == null)
        {
            GetComponent<TrailRenderer>().Clear();
            bulletPool.Relese(this);
        }
        else
            transform.position = Vector3.MoveTowards(transform.position, targetPos.position + new Vector3(0,0.5f,0), speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(damage);
            GetComponent<TrailRenderer>().Clear();
            bulletPool.Relese(this);
        }
    }
}
