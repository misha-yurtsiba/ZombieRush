using UnityEngine;
public interface IEnemyFactory
{
    public Enemy CreateEnemy(Vector3 spawnPos);
}
