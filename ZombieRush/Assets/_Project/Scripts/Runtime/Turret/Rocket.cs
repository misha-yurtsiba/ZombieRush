using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private Transform targetPos;
    private ObjectPool<Rocket> rocketPool;
    private float speed;
    private float damage;
    private float blastRadius;

    public void Init(Transform targetPos, ObjectPool<Rocket> rocketPool, float speed, float damage, float blastRadius)
    {
        this.targetPos = targetPos;
        this.rocketPool = rocketPool;
        this.speed = speed;
        this.damage = damage;
        this.blastRadius = blastRadius;
    }

    private void Update()
    {
        if (targetPos == null)
        {
            rocketPool.Relese(this);
        }
        else
            transform.position = Vector3.MoveTowards(transform.position, targetPos.position + new Vector3(0, 0.5f, 0), speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);
        
        foreach(Collider collider in colliders)
            if (collider.TryGetComponent(out Enemy enemy))
                enemy.TakeDamage(damage);

        rocketPool.Relese(this);
    }
}
