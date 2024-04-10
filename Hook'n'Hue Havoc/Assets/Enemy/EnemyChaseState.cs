using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{

    int moveSpeed = 4;
    float minDist = .3f;

    Vector3 playerPosition = new Vector3();

    public override void EnterState(EnemyStateManager enemy) {

    }

    public override void UpdateState(EnemyStateManager enemy) {
        
        playerPosition = GameObject.Find("PlayerPrefab").transform.position;

        if(Vector3.Distance(enemy.transform.position, playerPosition) > 15) {
            enemy.SwitchState(enemy.WanderState);
        } else {

            enemy.transform.LookAt(playerPosition);

            if(Vector3.Distance(enemy.transform.position, playerPosition) >= minDist) {
                enemy.transform.position += enemy.transform.forward * moveSpeed * Time.deltaTime;
            } else {
                enemy.gameplay.lost = true;
            }
        }


        

        
    }
}
