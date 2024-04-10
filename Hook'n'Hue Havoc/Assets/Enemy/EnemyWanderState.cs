using UnityEngine;
using UnityEngine.AI;

public class EnemyWanderState : EnemyBaseState
{

    float wanderRadius = 10;
    float wanderDistance = 10;
    float wanderJitter = 1;
    Vector3 wanderTarget = new Vector3();
    public NavMeshAgent agent;

    Vector3 playerPosition = new Vector3();

    public override void EnterState(EnemyStateManager enemy) {
        agent = enemy.GetComponent<NavMeshAgent>();
    }

    public override void UpdateState(EnemyStateManager enemy) {

        playerPosition = GameObject.Find("PlayerPrefab").transform.position;

        if(Vector3.Distance(enemy.transform.position, playerPosition) <= 15) {
            enemy.SwitchState(enemy.ChaseState);
        } else {
            Wander();
        }
    }

    void Wander() {
        wanderTarget += new Vector3(Random.Range(-1.0f, 1.0f) * wanderJitter, 0, Random.Range(-1.0f, 1.0f) * wanderJitter);

        wanderTarget.Normalize();
        wanderTarget *= wanderRadius;

        Vector3 targetLocal = wanderTarget + new Vector3(0, 0, wanderDistance);
        Vector3 targetWorld = agent.transform.InverseTransformVector(targetLocal);

        Seek(targetWorld);
    }

    void Seek(Vector3 location) {
        agent.SetDestination(location);
    }
}
