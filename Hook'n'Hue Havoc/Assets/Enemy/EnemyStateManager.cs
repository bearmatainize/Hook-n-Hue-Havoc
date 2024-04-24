using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{

    public Animator enemyAnim;

    EnemyBaseState currentState;

    public EnemyWanderState WanderState = new EnemyWanderState();
    public EnemyChaseState ChaseState = new EnemyChaseState();
    public EnemyShootState ShootState = new EnemyShootState();
    
    public BulletSpawner enemyBulletSpawner;

    // Start is called before the first frame update
    void Start()
    {
        currentState = WanderState;

        currentState.EnterState(this);

        enemyAnim.SetTrigger("Run");

    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(EnemyBaseState state) {
        currentState = state;
        state.EnterState(this);
    }
}
