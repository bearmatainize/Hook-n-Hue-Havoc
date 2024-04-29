using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    public float speed = 2f; // The speed of the enemy
    private Rigidbody rb; // The Rigidbody component

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

        if(Vector3.Distance(rb.position, playerPosition) > 15) {
            enemy.SwitchState(enemy.WanderState);
        } else {

            enemy.transform.LookAt(playerPosition);

            if(Vector3.Distance(rb.position, playerPosition) <= enemy.ShootState.shootingRange) {
                enemy.SwitchState(enemy.ShootState);
            } else {
                // Move towards the player
                Vector3 moveDirection = (playerPosition - rb.position).normalized;
                rb.MovePosition(rb.position + moveDirection * speed * Time.deltaTime);
            }
        }
    }
}