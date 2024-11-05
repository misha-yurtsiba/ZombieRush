using UnityEngine;

public class LaserTurret : Turret
{
    [SerializeField] private float damagePerSecond;
    [SerializeField] private float laserSpead = 2;

    private LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.sharedMaterial.SetFloat("_LaserSpead", laserSpead);
    }
    private void Update()
    {
        if (gamePaused) return;

        if (!isMoving && (IsEnemyInAttackRange() || IsEnemyFind()) && canAttack)
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

    protected override void IsGamePaused(bool isPaused)
    {
        base.IsGamePaused(isPaused);

        if (isPaused)
            lineRenderer.sharedMaterial.SetFloat("_LaserSpead", 0);
        else
            lineRenderer.sharedMaterial.SetFloat("_LaserSpead", laserSpead);
    }
}
