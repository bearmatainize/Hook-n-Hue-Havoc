using UnityEngine;

public class EnemyShootState : EnemyBaseState
{
    public float shootingRange = 10f;
    public float fireRate = 1f;
    private float lastFireTime;

    public override void EnterState(EnemyStateManager enemy)
    {
        lastFireTime = Time.time;
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        enemy.transform.LookAt(GameObject.Find("PlayerPrefab").transform.position);

        if (Vector3.Distance(enemy.transform.position, GameObject.Find("PlayerPrefab").transform.position) > shootingRange)
        {
            enemy.SwitchState(enemy.ChaseState);
        }
        else
        {
            if (Time.time > lastFireTime + 1/fireRate)
            {
                Shoot(enemy);
                lastFireTime = Time.time;
            }
        }
    }

    private void Shoot(EnemyStateManager enemy)
    {
        Vector3 shootDirection = (GameObject.Find("PlayerPrefab").transform.position - enemy.transform.position).normalized;
        enemy.enemyBulletSpawner.SpawnBullet(shootDirection);
    }
}