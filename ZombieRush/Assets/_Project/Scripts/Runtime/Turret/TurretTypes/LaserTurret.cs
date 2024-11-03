using UnityEngine;

public class LaserTurret : Turret
{
    [SerializeField] private float damagePerSecond;

    private LineRenderer lineRenderer;
    private float laserSpead;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        laserSpead = lineRenderer.sharedMaterial.GetFloat("_LaserSpead");
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

    protected override void IsGamePused(bool isPaused)
    {
        base.IsGamePused(isPaused);

        if (isPaused)
            lineRenderer.sharedMaterial.SetFloat("_LaserSpead", 0);
        else
            lineRenderer.sharedMaterial.SetFloat("_LaserSpead", laserSpead);
    }
}
