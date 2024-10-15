using UnityEngine;

public class LaserTurret : Turret
{
    [SerializeField] private float damagePerSecond;

    private LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
    private void Update()
    {
        if ((IsEnemyInAttackRange() || IsEnemyFind()) && canAttack)
            Shoot();
        else
            lineRenderer.enabled = false;

        base.Update();

    }

    private void Shoot()
    {
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, bulletSpawnPoint.position);
        lineRenderer.SetPosition(1, targetEnemy.transform.position + new Vector3(0,1,0));
        targetEnemy.TakeDamage(damagePerSecond * Time.deltaTime);
    }
}
