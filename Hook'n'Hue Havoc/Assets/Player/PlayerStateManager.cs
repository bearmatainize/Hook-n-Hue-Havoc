using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateManager : MonoBehaviour
{

    public FirstPersonController firstPersonController;
    public Animator playerAnim;
    public PlayerInput playerInput;
    public GrappleHook grappleHook;

    PlayerBaseState currentState;

    public PlayerIdleState IdleState = new PlayerIdleState();
    public PlayerRunState RunState = new PlayerRunState();
    public PlayerGrappleState GrappleState = new PlayerGrappleState();
    /*public PlayerJumpState JumpState = new PlayerJumpState();
    public PlayerFallState FallState = new PlayerFallState();*/


    // Start is called before the first frame update
    void Start()
    {
        currentState = IdleState;

        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(PlayerBaseState state) {
        currentState = state;
        state.EnterState(this);
    }
}
