using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIdleState : PlayerBaseState
{


    public override void EnterState(PlayerStateManager player) {
        player.playerAnim.ResetTrigger("Run");
        player.playerAnim.SetTrigger("Idle");
    }

    public override void UpdateState(PlayerStateManager player) {
        if(!Mathf.Approximately(player.firstPersonController.xInput, 0f) || !Mathf.Approximately(player.firstPersonController.zInput, 0f))
        {
            player.SwitchState(player.RunState);
        }
        // Check for input to fire the grapple hook
        if (player.playerInput.actions["Grapple"].IsPressed())
        {
            player.SwitchState(player.GrappleState);
        }
        
        if (player.playerInput.actions["Jump"].IsPressed() && player.firstPersonController.IsGrounded())
        {
            player.SwitchState(player.JumpState);
        }
    }
}
