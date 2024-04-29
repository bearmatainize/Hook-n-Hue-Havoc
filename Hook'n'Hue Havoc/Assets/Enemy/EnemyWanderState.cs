using UnityEngine;

public class EnemyWanderState : EnemyBaseState
{
    public float speed = 2f; // The speed of the enemy
    private Rigidbody rb; // The Rigidbody component

    float wanderRadius = 10;
    float wanderDistance = 10;
    float wanderJitter = 1;
    Vector3 wanderTarget = new Vector3();

    Vector3 playerPosition = new Vector3();

    public override void EnterState(EnemyStateManager enemy) {
        rb = enemy.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        rb.freezeRotation = true;
        enemy.enemyAnim.ResetTrigger("Run");
        enemy.enemyAnim.ResetTrigger("Idle");
        enemy.enemyAnim.SetTrigger("Run");
    }

    public override void UpdateState(EnemyStateManager enemy) {

        playerPosition = GameObject.Find("PlayerPrefab").transform.position;

        if(Vector3.Distance(rb.position, playerPosition) <= enemy.ShootState.shootingRange) {
            enemy.SwitchState(enemy.ShootState);
        } else if(Vector3.Distance(rb.position, playerPosition) <= 15) {
            enemy.SwitchState(enemy.ChaseState);
        } else {
            Wander(enemy);
        }
    }

    void Wander(EnemyStateManager enemy) {
        wanderTarget += new Vector3(Random.Range(-1.0f, 1.0f) * wanderJitter, 0, Random.Range(-1.0f, 1.0f) * wanderJitter);

        wanderTarget.Normalize();
        wanderTarget *= wanderRadius;

        Vector3 targetLocal = wanderTarget + new Vector3(0, 0, wanderDistance);
        Vector3 targetWorld = enemy.transform.InverseTransformVector(targetLocal);

        Seek(targetWorld, enemy);
    }

    void Seek(Vector3 location, EnemyStateManager enemy) {
        Vector3 moveDirection = (location - rb.position).normalized;
        rb.MovePosition(rb.position + moveDirection * speed * Time.deltaTime);
        
    }
}